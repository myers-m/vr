using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(this.Onclick);
    }

    void Onclick() {
        print("this");
        switch (this.name) {
            case "review":
                print("还未实装");
                break;

            case "exit":
                Gamemanager.instance.exit();
                break;
        }
    }
}
