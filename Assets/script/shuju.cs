using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shuju : MonoBehaviour
{
    public static shuju instance;

    public scenemanager manager;
    public player _player;
    public controller _controller;
    public GameObject controller;
    public GameObject _fire;

    public float time = 300;

    public int firestep = 1;

    public string ray = "";

    public bool pause = false;
    private bool dopausebl = false;
    private bool beginbl = false;
    private bool finishbl = false;

    private bool dohc = false;

    public int _finish = 0;

    public bool dopause {
        get { return dopausebl; }
        set { switch (value) {
                case true:
                    pause = true;
                    switch (dohc) {
                        case false:
                            Invoke("timeset",1000);
                            dohc = true;
                            break;
                    }
                    break;
                case false:
                    dopausebl = false;
                    pause = false;
                    beginbl = false;
                    finishbl = false;
                    break;
            } }
    }

    public bool begin {
        get { return beginbl; }
        set { beginbl = value; pause = value; }
    }

    public bool finish {
        get { return finishbl; }
        set { finishbl = value;pause = value; }
    }
    


    private void Awake()
    {
        if (shuju.instance == null) {
            shuju.instance = this;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (!this.pause) {
            case true:
                shuju.instance.time -= Time.deltaTime;
                break;
        }
    }

    void timeset() {
        dohc = false;
        dopausebl = true;
    }
}
