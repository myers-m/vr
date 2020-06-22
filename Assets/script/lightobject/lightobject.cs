using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightobject : MonoBehaviour
{
    string gname = "";

    bool glight = false;

    // Start is called before the first frame update
    void Start()
    {
        this.gname = this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        switch (shuju.instance.ray==this.gname) {
            case true:
                SetColor(true);
                break;

            case false:
                SetColor(false);
                break;
        }
    }

    void SetColor(bool need)
    {
        switch (need) {
            case true:
                switch (this.glight) {
                    case false:
                        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
                        GetComponent<Renderer>().GetPropertyBlock(propertyBlock);
                        propertyBlock.SetFloat("_otherset", 1);
                        GetComponent<Renderer>().SetPropertyBlock(propertyBlock);
                        this.glight = true;
                        break;
                }
                break;

            case false:
                switch (this.glight) {
                    case true:
                        MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
                        GetComponent<Renderer>().GetPropertyBlock(propertyBlock);
                        propertyBlock.SetFloat("_otherset", 0);
                        GetComponent<Renderer>().SetPropertyBlock(propertyBlock);
                        this.glight = false;
                        break;
                }
                break;
        
        }
    }
}
