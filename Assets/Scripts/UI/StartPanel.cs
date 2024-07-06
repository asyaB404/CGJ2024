using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : PanelBase
{
    public Button ExitButton;
    public Button GameButton;
    protected override void Init()
    {
        ExitButton.onClick.AddListener(()=>Application.Quit());
        GameButton.onClick.AddListener(GameStart);
    }
    public void GameStart(){

    }
}
