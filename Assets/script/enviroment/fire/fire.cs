using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class fire : MonoBehaviour
{
    public GameObject firelight;
    public GameObject smoke;

    int firestep = 1;

    // Start is called before the first frame update
    void Start()
    {
        shuju.instance._fire = this.gameObject;
        this.gameObject.GetComponent<ParticleSystem>().gravityModifier = 0.01f;
        firelight.GetComponent<Light>().intensity = 1;
        this.smoke.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        this.SetFire();
    }

    void SetFire() {
        switch (shuju.instance.firestep!=this.firestep) {
            case true:
                switch (shuju.instance.firestep) {
                    case 1:
                        this.gameObject.GetComponent<ParticleSystem>().gravityModifier = 0.01f;
                        firelight.GetComponent<Light>().intensity = 1;
                        this.smoke.SetActive(false);
                        break;

                    case 2:
                        this.gameObject.GetComponent<ParticleSystem>().gravityModifier = 0.07f;
                        firelight.GetComponent<Light>().intensity = 2;
                        break;

                    case 3:
                        this.gameObject.GetComponent<ParticleSystem>().gravityModifier = 0.2f;
                        firelight.GetComponent<Light>().intensity = 3;
                        this.smoke.SetActive(true);
                        shuju.instance._finish += 1;
                        break;
                }
                this.firestep = shuju.instance.firestep;
                break;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        switch (this.firestep != 3)
        {
            case true:
                switch (collision.gameObject.tag)
                {
                    case "firewood":
                        switch (collision.gameObject.GetComponent<firewood>().usestep)
                        {
                            case 1:
                                shuju.instance.manager.SetWaring("很好的燃烧材料，火似乎更旺了呢！");
                                shuju.instance.firestep += 1;
                                Destroy(collision.gameObject);
                                break;

                            case 2:
                                shuju.instance.manager.SetWaring("潮湿的木柴使火势衰弱了不少。");
                                shuju.instance.firestep = shuju.instance.firestep == 1 ? 1 : shuju.instance.firestep - 1;
                                Destroy(collision.gameObject);
                                break;

                            case 3:
                                shuju.instance.manager.SetWaring("潮湿乃至湿透的木柴是燃烧不了的，还是放在火边烤一烤吧？");
                                break;
                        }
                        break;
                }
                break;

            case false:
                shuju.instance.manager.SetWaring("火已经足够旺盛\n继续添柴可能会引发火灾！");
                break;
        }
    }
}
