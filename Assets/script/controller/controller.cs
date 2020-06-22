using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    WaveVR_Controller.Device device;

    // Start is called before the first frame update
    void Start()
    {
        this.device = WaveVR_Controller.Input(WaveVR_Controller.EDeviceType.Dominant);
        shuju.instance._controller = this;
    }

    // Update is called once per frame
    void Update()
    {
        switch (shuju.instance.begin || shuju.instance.finish) {
            case false:
                this.DoPlayerController();
                this.DoOtherController();
                break;
        }
    }

    void DoPlayerController() {
        switch (this.device.GetTouch(wvr.WVR_InputId.WVR_InputId_Alias1_Touchpad)) {
            case true:
                Vector2 move = this.device.GetAxis(wvr.WVR_InputId.WVR_InputId_Alias1_Touchpad);
                shuju.instance._player._move.Set(move.x, 0, move.y);
                break;
        }
        switch (this.device.GetPressDown(wvr.WVR_InputId.WVR_InputId_Alias1_Trigger)||Input.GetKeyDown(KeyCode.K))
        {
            case true:
                shuju.instance._player._pick = true;
                break;
        }
    }

    void DoOtherController() {
        switch (this.device.GetPressDown(wvr.WVR_InputId.WVR_InputId_Alias1_Menu))
        {
            case true:
                Gamemanager.instance.exit();
                break;
        }
    }
}
