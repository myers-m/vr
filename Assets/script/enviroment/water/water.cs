using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag) {
            case "player":
                shuju.instance.manager.SetWaring("下雪时，水体冰冷，若是久待可能会患上低温症！");
                break;
        }
    }
}
