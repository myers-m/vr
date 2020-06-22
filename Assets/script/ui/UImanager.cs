using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public GameObject firewoodtext1;
    public GameObject firewoodtext2;
    public GameObject waringtext;
    public GameObject review;
    public GameObject exit;

    bool firewood = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.SetFirewoodText();
        this.SetWaringText();
        this.SetButton();
    }

    void SetFirewoodText() {
        switch (shuju.instance._player._choose||shuju.instance._player._have) {
            case true:
                switch (this.firewood) {
                    case false:
                        string need1="";
                        string need2="潮湿度："+(int)shuju.instance._player._chooseobj.GetComponent<firewood>().moistrue;
                        Vector3 color = new Vector3(101, 101, -154);
                        switch (shuju.instance._player._chooseobj.GetComponent<firewood>().usestep) {
                            case 1:
                                need1 = "一份干燥的木柴";
                                break;

                            case 2:
                                need1 = "一份潮湿的木柴";
                                break;

                            case 3:
                                need1 = "一份湿透的木柴";
                                break;
                        }
                        color *= (float)(100 - shuju.instance._player._chooseobj.GetComponent<firewood>().moistrue) / 100.0f;
                        color += new Vector3(154, 154, 154);
                        this.firewoodtext1.GetComponent<Text>().text = need1;
                        this.firewoodtext1.GetComponent<Text>().color = new Color(color.x, color.y, color.z, 1);
                        this.firewoodtext2.GetComponent<Text>().text = need2;
                        this.firewood = true;
                        break;
                }
                break;

            case false:
                this.firewood = false;
                this.firewoodtext1.GetComponent<Text>().text = "";
                this.firewoodtext2.GetComponent<Text>().text = "";
                break;
        }
    }

    void SetWaringText() {
        switch (shuju.instance.manager.waringbl)
        {
            case true:
                this.waringtext.GetComponent<Text>().text = shuju.instance.manager.waring;
                this.waringtext.GetComponent<Text>().color = new Color(1, 1, 1, Math.Min(1, shuju.instance.manager.time / 1.0f));
                break;

            case false:
                this.waringtext.GetComponent<Text>().text = "";
                break;
        }
    }

    void SetButton() {
        switch (shuju.instance.finish&&!this.review.active&&!this.exit.active) {
            case true:
                this.review.SetActive(true);
                this.exit.SetActive(true);
                break;
        }
    }

}
