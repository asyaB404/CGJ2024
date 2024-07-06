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
    protected override void Init()
    {
        menu.onClick.AddListener(()=>{UIManager.Instance.ShowPanel<StopPanel>();
            Debug.Log("sdaasadd");
        });
    }
    
}
