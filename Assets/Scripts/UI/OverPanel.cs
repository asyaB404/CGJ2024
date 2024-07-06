using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverPanel : PanelBase
{
    public Button again;
    public Button goHome;
    protected override void Init()
    {
        again.onClick.AddListener(()=>SceneManager.LoadScene("Game"));
        goHome.onClick.AddListener(()=>SceneManager.LoadScene("Home"));
    }
}
