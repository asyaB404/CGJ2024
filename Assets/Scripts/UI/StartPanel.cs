using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanel : PanelBase
{
    public Button GameButton;

    protected override void Init()
    {
        GameButton.onClick.AddListener(GameStart);
        MusicMgr.Instance.PlayBkMusic("bgm");
    }

    public void GameStart()
    {
        MySceneManager.MSceneManager("Game");
    }
}