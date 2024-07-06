using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel<BeginPanel>
{
    protected override void Awake()
    {
        base.Awake();
        GetControl<Button>("BeginBtk").onClick.AddListener(() =>
        {
            //开始游戏
        }); 
        GetControl<Button>("SettingBtk").onClick.AddListener(() =>
        {
            //打开设置面板
        }); 
        GetControl<Button>("QuitBtk").onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    public override void HideMe()
    {
        base.HideMe();

    }

    public override void ShowMe()
    {
        base.ShowMe();

    }
}
