using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StopPanel : PanelBase
{
    public Button GoButton;
    public Button RestartButton;
    public Button GoBackButton;
    private bool isMyFade=false;
    protected override void Init()
    {
        GoButton.onClick.AddListener(()=>{
            Time.timeScale=1;
            UIManager.Instance.HidePanel<StopPanel>();
        });
        RestartButton.onClick.AddListener(()=>MySceneManager.MSceneManager("Game"));
        GoBackButton.onClick.AddListener(()=>MySceneManager.MSceneManager("Home"));
    }
    public override void ShowMe()
    {
        print(isMyFade);
        if(isMyFade){
            HideMe();
            return;
        }
        base.ShowMe();
        isMyFade=true;
        Time.timeScale=0;
    }
    public override void HideMe(UnityAction callBackAction = null)
    {
        base.HideMe(callBackAction);
        isMyFade=false;
        Time.timeScale=1;
        Debug.Log("关闭了"+Time.timeScale);
    }
}
