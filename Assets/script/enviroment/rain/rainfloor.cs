using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rainfloor : MonoBehaviour
{
    ComputeShader _compute;
    // Start is called before the first frame update
    void Start()
    {
        this._compute.FindKernel("rainpoint");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
