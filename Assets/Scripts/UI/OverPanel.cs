using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverPanel : PanelBase
{
    public Button again;
    public Button goHome;
    [SerializeField] private Text score;

    protected override void Init()
    {
        again.onClick.AddListener(() => MySceneManager.MSceneManager("Game"));
        goHome.onClick.AddListener(() => MySceneManager.MSceneManager("Home"));
        score.text = ":" + Player.Instance.Score;
    }
}