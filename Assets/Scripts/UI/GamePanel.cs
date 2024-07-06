using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class GamePanel : BasePanel<GamePanel>
{
    public enum E_ShowImageType
    {
        fristLine,
        secondLine,
        thirdLine,
        fourthLine,
    }

    [Header("顾客图片显示位置的偏移量")]
    public float offset;
    [Tooltip("显隐速度")]
    public float fadeSpeed;

    private Color unit;

    private Dictionary<E_ShowImageType,Image> showImagesDic = new Dictionary<E_ShowImageType, Image>();
    private Image curImage;

    private bool isShowImage;
    private bool isHideImage;

    private UnityAction afterShowImageEvent;
    private UnityAction afterHideImageEvent;
    protected override void Awake()
    {
        base.Awake();

        for(int i = 0;i<4;i++)
            showImagesDic.Add((E_ShowImageType)i,GetControl<Image>("ShowImage" + (i + 1)));
        //foreach (Image image in showImagesDic.Values)
        //    image.color = new Color(1, 1, 1, 0);
        foreach (E_ShowImageType e in showImagesDic.Keys)
            DelShowImage(e, null);

        //事件监听
        MyEventSystem.Instance.AddEventListener<float>(EventKeyNameData.UI_UpdateTimebar, UpdateTimeBar);
        MyEventSystem.Instance.AddEventListener<int>(EventKeyNameData.UI_UpdateOverNum, UpdateOverNum);
        MyEventSystem.Instance.AddEventListener<int>(EventKeyNameData.UI_UpdateToDoNUm, UpdatetoDoNum);
        MyEventSystem.Instance.AddEventListener<E_ShowImageType,UnityAction>(EventKeyNameData.UI_DelImage, DelShowImage);
        MyEventSystem.Instance.AddEventListener<ShowImageData,E_ShowImageType,UnityAction>(EventKeyNameData.UI_ShowImage, NPCShowImage);

    }

    private void Update()
    {
        if(isShowImage && curImage.color.a < 1)
        {
            curImage.color += unit*fadeSpeed*Time.deltaTime;
            if(curImage.color.a >= 1)
            {
                isShowImage = false;
                curImage.color = new Color(1, 1, 1, 1);
                afterShowImageEvent?.Invoke();
            }

        }
        else if (isHideImage && curImage.color.a > 0)
        {
            curImage.color -= unit*fadeSpeed*Time.deltaTime;
            if(curImage.color.a <= 0)
            {
                isHideImage = false;
                curImage.color = new Color(1, 1, 1, 0);
                curImage.sprite = null;
                afterHideImageEvent?.Invoke();
            }
        }
    }
    /// <summary>
    /// 更新时间进度的UI显示
    /// </summary>
    /// <param name="ratio">时间比值</param>
    public void UpdateTimeBar(float ratio)
    {
        GetControl<Image>("TimeBar").fillAmount = ratio;
    }

    /// <summary>
    /// 更新完成数量显示
    /// </summary>
    /// <param name="num">完成数量</param>
    public void UpdateOverNum(int num)
    {
        GetControl<Text>("OverNumText").text = num.ToString();
    }
    /// <summary>
    /// 更新待取餐数量
    /// </summary>
    /// <param name="num">待取餐数量</param>
    public void UpdatetoDoNum(int num)
    {
        GetControl<Text>("ToDoNumText").text = num.ToString();
    }

    /// <summary>
    /// 顾客头顶显示Image
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="type">图像显示在第几行</param>
    public void NPCShowImage(ShowImageData data,E_ShowImageType type,UnityAction callBack = null)
    {
        if (showImagesDic.ContainsKey(type))
        {
            isShowImage = true;
            showImagesDic[type].sprite = data.showImage;
            showImagesDic[type].color = new Color(1, 1, 1, 0);
            curImage = showImagesDic[type];
            Vector3 pos = Camera.main.WorldToScreenPoint(data.showPos);
            showImagesDic[type].transform.position = pos + Vector3.up * offset;
            afterShowImageEvent = callBack;
        }
        else
            Debug.Log($"没有改类型: {type} 的图片");
    }

    /// <summary>
    /// 消除顾客头顶显示Image
    /// </summary>
    /// <param name="type">图像显示在第几行</param>
    public void DelShowImage(E_ShowImageType type,UnityAction callBack)
    {
        if( showImagesDic.ContainsKey(type))
        {
            isHideImage = true;
            showImagesDic[type].color = new Color(1, 1, 1, 1);
            curImage = showImagesDic[type];
            afterHideImageEvent = callBack;
        }
    }

    private void OnDestroy()
    {
        MyEventSystem.Instance.RemoveEventListener<float>(EventKeyNameData.UI_UpdateTimebar, UpdateTimeBar);
        MyEventSystem.Instance.RemoveEventListener<int>(EventKeyNameData.UI_UpdateOverNum, UpdateOverNum);
        MyEventSystem.Instance.RemoveEventListener<int>(EventKeyNameData.UI_UpdateToDoNUm, UpdatetoDoNum);
        MyEventSystem.Instance.RemoveEventListener<E_ShowImageType, UnityAction>(EventKeyNameData.UI_DelImage, DelShowImage);
        MyEventSystem.Instance.RemoveEventListener<ShowImageData, E_ShowImageType, UnityAction>(EventKeyNameData.UI_ShowImage, NPCShowImage);
    }
}
