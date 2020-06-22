using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class player : MonoBehaviour
{
    CharacterController m_ch;
    public GameObject head;
    public GameObject wave;

    float speed = 4.0f;
    float gravity = 9.8f;
    public float yspeed = 0;

    public bool _have = false;
    public GameObject _pickobj;

    public bool _pick = false;
    public Vector3 _move = new Vector3(0,0,0);


    public bool _choose = false;
    public GameObject _chooseobj;

    public float time = 0;

    RaycastHit hitinfo;

    // Start is called before the first frame update
    void Start()
    {
        this.m_ch = this.GetComponent<CharacterController>();
        this.m_ch.transform.localScale.Set(0.5f, 0.5f, 0.5f);
        shuju.instance._player = this;
    }

    // Update is called once per frame
    void Update()
    {
        switch (!shuju.instance.pause) {
            case true:
                this.Move();
                this.Ray();
                this.Sync();
                break;
        }
        this.Clean();
    }

    void Move() {
        this.ComputeGravity();
        this._move = this.transform.TransformDirection(this._move);
        this._move *= this.speed;
        this._move.y = this.yspeed;
        this.m_ch.Move(this._move * Time.deltaTime);
    }

    void Ray() {
        bool need = Physics.Raycast(shuju.instance.controller.transform.position, shuju.instance.controller.transform.forward, out this.hitinfo, 3);
        switch (need)
        {
            case true:
                switch (this.hitinfo.collider.tag) {
                    case "firewood":
                        this._chooseobj = this._have ? this._chooseobj : this.hitinfo.collider.gameObject;
                        this._choose = true;
                        shuju.instance.ray = this.hitinfo.collider.gameObject.name;
                        this.Pick();
                        break;

                    default:
                        switch (this._have&&this._pick) {
                            case true:
                                this.PickOut();
                                this._pick = false;
                                break;

                            case false:
                                shuju.instance.ray = "";
                                break;
                        }
                        this._choose = false;
                        break;
                }
                break;

            case false:
                this._choose = false;
                shuju.instance.ray = "";
                break;
        }
    }

    Vector3 high = new Vector3(0, 0.5f, 0);
    void Sync() {
        this.high.y = 0.5f + (float)Math.Sin(this.time * Math.PI) * 0.1f;
        this.wave.transform.position = this.transform.position + this.high;
        this.transform.eulerAngles = new Vector3(0,this.head.transform.rotation.eulerAngles.y,0);
        this.time += Time.deltaTime;
    }

    void Clean()
    {
        this._move.Set(0, 0, 0);
        this._pick = false;
    }

    void Pick() {
        switch (this._pick) {
            case true:
                switch (this._have) {
                    case false:
                        this.PickIn();
                        break;
                }
                this._pick = false;
                break;
        }
    }

    void PickIn() {
        this._pickobj = this.hitinfo.collider.gameObject;
        this._pickobj.SetActive(false);
        this._have = true;
        print("pickin");
    }

    void PickOut() {
        this._pickobj.transform.position = this.hitinfo.point + this.high;
        this._pickobj.SetActive(true);
        this._pickobj = null;
        this._have = false;
        print("pickout");
    }

    void ComputeGravity()
    {
        switch (this.m_ch.isGrounded) {
            case true:
                this.yspeed = 0;
                break;

            case false:
                this.yspeed -= this.gravity * Time.deltaTime; ;
                break;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch (hit.gameObject.tag == "Fire")
        {
            case true:
                shuju.instance.manager.SetWaring("请与火源保持距离，避免烧伤");
                break;
        }
    }
}
