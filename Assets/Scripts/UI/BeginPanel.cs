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
            //��ʼ��Ϸ
        }); 
        GetControl<Button>("SettingBtk").onClick.AddListener(() =>
        {
            //���������
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
