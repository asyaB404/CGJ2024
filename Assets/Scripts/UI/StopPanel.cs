using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StopPanel : PanelBase
{
    public Button GoButton;
    public Button RestartButton;
    public Button GoBackButton;
    protected override void Init()
    {
        GoButton.onClick.AddListener(()=>{
            Time.timeScale=1;
            UIManager.Instance.HidePanel<StopPanel>();
        });
        RestartButton.onClick.AddListener(()=>SceneManager.LoadScene("Game"));
        GoBackButton.onClick.AddListener(()=>SceneManager.LoadScene("Home"));
    }
}
