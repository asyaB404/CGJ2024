using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel<BeginPanel>
{
    private Text gameName;
    private Button beginGameBtk;
    private Button settingBtk;
    private Button quitBtk;

    protected override void Awake()
    {
        base.Awake();

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
