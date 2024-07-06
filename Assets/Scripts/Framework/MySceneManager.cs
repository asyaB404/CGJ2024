using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class MySceneManager 
{
    public static void MSceneManager(string name){
        UIManager.Instance.Clear();
        SceneManager.LoadScene(name);
        PoolManager.Instance.Clear();
    }
    public static void MSceneManager(int i){
        UIManager.Instance.Clear();
        PoolManager.Instance.Clear();
        SceneManager.LoadScene(i);
    }
}
