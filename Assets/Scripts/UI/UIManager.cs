using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            instance=new();
            
            return instance;
        }
    }
    private Dictionary<string, PanelBase> panelDic = new();
    private Transform canvasTrans;


    private UIManager()
    {
        GetCanvas();
    }
    /// <summary>
    /// 生成并获取一个面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T ShowPanel<T>() where T : PanelBase
    {
        string panelName = typeof(T).Name;
        return ShowPanel<T>(panelName);
    }
    private void GetCanvas(){
        GameObject canvasOBJ = GameObject.Find("Canvas");
        if(canvasOBJ==null){
            canvasTrans=PoolManager.Instance.Get("Canvas").transform;
            canvasTrans.name="Canvas";
        }
        else canvasTrans=canvasOBJ.transform;
    }
    public T ShowPanel<T>(string panelName) where T : PanelBase
    {
        if(!canvasTrans)GetCanvas();
        if (panelDic.ContainsKey(panelName)) return panelDic[panelName] as T;
        GameObject panelObj = PoolManager.Instance.Get("UI/Panel/" + panelName);//GameObject.Instantiate(Resources.Load<GameObject>("UI/"+panelName));
        panelObj.transform.SetParent(canvasTrans, false);
        T panel = panelObj.GetComponent<T>();
        if (panel == null) panel = panelObj.AddComponent<T>();
        panelDic.Add(panelName, panel);
        panel.ShowMe();
        return panel;
    }
    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="isFade">是否需要淡入淡出</param>
    public void HidePanel<T>(bool isFade = false) where T : PanelBase
    {
        string panelName = typeof(T).Name;
        HidePanel(panelName, isFade);
    }
    public void HidePanel(string panelName, bool isFade = false)
    {

        if (panelDic.ContainsKey(panelName))
        {
            if (isFade) 
            panelDic[panelName].HideMe(() =>{PoolManager.Instance.Push(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);});
            else
            {
                panelDic[panelName].HideMe();
                PoolManager.Instance.Push(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
        }
    }
    /// <summary>
    /// 仅返回不生成,若没有返回空
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetPanel<T>() where T : PanelBase
    {
        string panelName = typeof(T).Name;
        return GetPanel<T>(panelName);
    }
    public T GetPanel<T>(string panelName) where T : PanelBase
    {
        if (panelDic.ContainsKey(panelName)) return panelDic[panelName] as T;
        return null;
    }
    public void Clear()
    {
        panelDic.Clear();
        // List<string> keys = new();
        // foreach (string key in panelDic.Keys)
        // {
        //     keys.Add(key);
        // }
        // foreach (string key in keys)
        // {
        //     HidePanel(key,false);
        // }

    }


}
