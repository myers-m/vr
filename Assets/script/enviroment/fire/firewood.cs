using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firewood : MonoBehaviour
{
    public float moistrue = 100;
    public float usestep = 3;


    private void Update()
    {
        switch (Vector3.Distance(shuju.instance._fire.transform.position, this.gameObject.transform.position) <= 5 && this.moistrue != 0)
        {
            case true:
                this.moistrue -= Time.deltaTime;
                switch (this.moistrue < 0)
                {
                    case true:
                        this.moistrue = 0;
                        this.usestep = 1;
                        shuju.instance._finish += 1;
                        break;

                    case false:
                        switch (usestep == 3 && this.moistrue < 50)
                        {
                            case true:
                                this.usestep = 2;
                                break;
                        }
                        break;
                }
                break;
        }
    }
}
