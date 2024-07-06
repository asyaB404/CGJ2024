using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public abstract class PanelBase : MonoBehaviour
{
    public float speed=10;
    private bool isHide=false;
    private UnityAction unityAction;
    public CanvasGroup canvasGroup;
    protected virtual void Awake() {
        canvasGroup=GetComponent<CanvasGroup>();
        if(canvasGroup==null)canvasGroup=gameObject.AddComponent<CanvasGroup>();
    }
    protected abstract void Init();
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Init();
    }
    public virtual void ShowMe(){
        isHide=false;
        canvasGroup.alpha=0;
    }
    // Update is called once per frame
    public virtual void HideMe(UnityAction callBackAction=null){
        isHide=true;
        canvasGroup.alpha=1;
        unityAction=callBackAction;
    }
    protected virtual void Update()
    {
        if(isHide){
            if(canvasGroup.alpha!=0)
                canvasGroup.alpha-=speed*Time.deltaTime;
            if(canvasGroup.alpha<=0){
                canvasGroup.alpha=0;
            unityAction?.Invoke();}
        }
        if(!isHide){
            if(canvasGroup.alpha!=1)
                canvasGroup.alpha+=speed*Time.deltaTime;
            if(canvasGroup.alpha>=1)canvasGroup.alpha=1;
        }
    }
}