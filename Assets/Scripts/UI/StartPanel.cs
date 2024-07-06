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
        Debug.Log("这一步也实现了");
        ExitButton.onClick.AddListener(()=>{
            Debug.Log("ssssdada时代");
            SceneManager.LoadScene("Game");
        });
        GameButton.onClick.AddListener(GameStart);
    }
    public void GameStart(){
        Debug.Log("sss");
    }
}
