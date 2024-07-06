using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel_ : PanelBase
{
    public GameObject sideOBJ;
    public Slider health;
    public Text score;
    public Text waiter;
    public Button menu;

    public List<Sprite> sprites;
    protected override void Init()
    {
        menu.onClick.AddListener(()=>{UIManager.Instance.ShowPanel<StopPanel>();
            Debug.Log("sdaasadd");
        });
    }

    protected override void Update()
    {
        base.Update();
        int CustCount=CustomerMgr.Instance.Count;
        waiter.text="待取餐人数："+CustCount;
        if(CustCount>20)sideOBJ.GetComponent<UnityEngine.UI.Image>().sprite=sprites[1];
        else sideOBJ.GetComponent<UnityEngine.UI.Image>().sprite=sprites[0];
    }
}
