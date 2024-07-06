using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanel : PanelBase
{
    public Button ExitButton;
    public Button GameButton;
    protected override void Init()
    {
        ExitButton.onClick.AddListener(()=>MySceneManager.MSceneManager("Home"));
        GameButton.onClick.AddListener(GameStart);
    }
    public void GameStart(){
        MySceneManager.MSceneManager("Game");
        
    }
}
