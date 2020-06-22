using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class scenemanager : MonoBehaviour
{
    public GameObject rain;

    public string waring = "";

    public bool waringbl = false;

    public float time = 3;

    bool timewaring = true;

    private void Start()
    {
        shuju.instance.manager = this;
        this.waring = "你流落到了野外，所幸在一段寻找后你发现了一个小屋\n但这时又下起了大雪，生起的火源将要熄灭，快去寻找可用的木柴吧\n雪将越下越大，请在5分钟内完成生火和必要木柴的存储。";
        this.waringbl = true;
    }

    // Update is called once per frame
    void Update()
    {
        switch (shuju.instance.time <= 0 || shuju.instance._finish == 5)
        {
            case true:
                this.waring = "你的得分为："+(shuju.instance._finish*20)+"\n你的成功添柴"+(shuju.instance.firestep-1)+"次\n你成功烘干了";
                this.waring += shuju.instance.firestep == 3 ? (shuju.instance._finish - shuju.instance.firestep + 2) + "份木头" : shuju.instance._finish + "份木头";
                this.waring += shuju.instance._finish == 5 ? "\n你的评价为优秀" : shuju.instance._finish > 3 ? "\n你的评价为一般" : "\n你的评价为差";
                this.waringbl = true;
                this.time = 3600;
                shuju.instance.finish = true;
                break;

            case false:
                switch (this.waringbl)
                {
                    case true:
                        this.time -= Time.deltaTime;
                        switch (this.time <= 0)
                        {
                            case true:
                                this.waringbl = false;
                                break;
                        }
                        break;

                    case false:
                        this.Judge();
                        break;
                }
                break;
        }
    }

    void Judge() {
        switch (shuju.instance._player.yspeed <= -8) {
            case true:
                this.SetWaring("下雪地滑，请不要从高空挑落");
                break;

            case false:
                switch (shuju.instance.time<=150&&this.timewaring) {
                    case true:
                        this.timewaring = false;
                        this.SetWaring("时间已过去一半，请尽快完成任务");
                        break;
                }
                break;
        }
    }

    public void SetWaring(string need) {
        this.waring = need;
        this.waringbl = true;
        this.time = 1.5f;
    }
}
