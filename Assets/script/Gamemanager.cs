using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    private void Awake()
    {
        if (Gamemanager.instance == null) {
            Gamemanager.instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void exit() {
        Application.Quit();
    }
}
