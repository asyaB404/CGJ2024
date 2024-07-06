using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class GamePanel : BasePanel<GamePanel>
{
    //测试内容
    public Transform test;
    //
    private Dictionary<int,CusUIInfo> showImagesDic = new Dictionary<int, CusUIInfo>();
    private int id = 0;
    //连击特效时间
    [Tooltip("连击特效存在时间")]
    public float stayTime = 0.5f;
    //连击特效放大倍数
    [Tooltip("连击图放大倍数")]
    public float Multip = 1.5f;
    [Tooltip("放大速度")]
    public float speed = 1;
    private float curTime;
    private bool isShowEffector;
    private bool isStaye;
    private Vector3 scale;
    private Image effector;
    private void Start()
    {
        scale = Vector3.one* Multip;
        //事件监听
        MyEventSystem.Instance.AddEventListener<int>(EventKeyNameData.UI_UpdateMoney, UpdateMoney);
        MyEventSystem.Instance.AddEventListener<int>(EventKeyNameData.UI_UpdateOverNum, UpdateOverNum);
        MyEventSystem.Instance.AddEventListener<int>(EventKeyNameData.UI_UpdateToDoNUm, UpdatetoDoNum);
        MyEventSystem.Instance.AddEventListener<Transform, DishType>(EventKeyNameData.UI_ShowImage, ShowCustomUI);
        MyEventSystem.Instance.AddEventListener<IGetUI_ID,UnityAction>(EventKeyNameData.UI_DelImage,HideCustomUI);
        MyEventSystem.Instance.AddEventListener<IGetUI_ID,bool>(EventKeyNameData.UI_ChangeImage,ChangeCustomUI);
        MyEventSystem.Instance.AddEventListener<Vector2, Sprite>(EventKeyNameData.UI_ShowEffector, ShowLianJiEffector);
    }

    private void Update()
    {
        //测试内容
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShowCustomUI(test, DishType.Dish1);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeCustomUI(test.GetComponent<IGetUI_ID>(), true);
        }
        if(Input.GetKeyDown (KeyCode.Alpha3))
        {
            HideCustomUI(test.GetComponent<IGetUI_ID>(), () =>
            {
                Destroy(test.gameObject);
            });
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
            ShowLianJiEffector(new Vector2(Screen.width/4, Screen.height/4),Resources.Load<Sprite>("UI/史莱姆王心脏"));
        //

        if(isShowEffector)
        {
            effector.transform.localScale = Vector3.Lerp(effector.transform.localScale,scale,Time.deltaTime*speed);
            if(Vector3.Distance(effector.transform.localScale , scale) < 0.01f)
            {
                isShowEffector = false;
                isStaye = true;
            }
        }
        else if(isStaye)
        {
            curTime += Time.deltaTime;
            if (curTime >= stayTime)
            {
                curTime = 0;
                isStaye = false;
                effector.color = new Color(1, 1, 1, 0);
            }

        }
    }

    /// <summary>
    /// 连击特效显示
    /// </summary>
    /// <param name="pos">位置,一屏幕中心为原点</param>
    /// <param name="sprite">图片</param>
    private void ShowLianJiEffector(Vector2 pos ,Sprite sprite)
    {
        effector = GetControl<Image>("Effector");
        if(effector == null)
        {
            Debug.LogError("控件：Effector 不存在,请添加名字为 Effector 的Image组件");
        }
        effector.color = new Color(1, 1, 1, 1);
        effector.sprite = sprite;
        effector.transform.localPosition = pos;
        effector.transform.localScale = Vector3.one;
        curTime = 0;
        isShowEffector = true;
        isStaye = false;
    }

   

    private void UpdateMoney(int money)
    {
        GetControl<Text>("MoneyText").text = money.ToString();
    }

    /// <summary>
    /// 更新完成数量显示
    /// </summary>
    /// <param name="num">完成数量</param>
    private void UpdateOverNum(int num)
    {
        GetControl<Text>("OverNumText").text = num.ToString();
    }
    /// <summary>
    /// 更新待取餐数量
    /// </summary>
    /// <param name="num">待取餐数量</param>
    private void UpdatetoDoNum(int num)
    {
        GetControl<Text>("ToDoNumText").text = num.ToString();
    }

    /// <summary>
    /// 显示气泡
    /// </summary>
    /// <param name="trans">顾客的Tranform</param>
    /// <param name="type">Dish类型</param>
    private void ShowCustomUI(Transform trans,DishType type)
    {
        GameObject obj = Resources.Load<GameObject>("Prefabs/CustomerUI");
        obj = Instantiate(obj);
        obj.transform.SetParent(transform,true);
        obj.transform.localPosition = Vector3.zero;
        CusUIInfo info = obj.GetComponent<CusUIInfo>();
        if (id == 12)
            id = 0;
        info.id = id++;
        IGetUI_ID getId;
        if (!trans.TryGetComponent<IGetUI_ID>(out getId))
            Debug.LogError("请确定传入对象使用了——" + typeof(IGetUI_ID).Name + "——接口");
        else
            getId.UI_ID = info.id;
        info.Show(trans, type);
        if (!showImagesDic.ContainsKey(info.id))
            showImagesDic.Add(info.id, info);
        else
            Debug.LogError(id + "改建已存在");
    }

    /// <summary>
    /// 隐藏气泡
    /// </summary>
    /// <param name="i">ID接口</param>
    /// <param name="callBack">顾客消失回调函数</param>
    private void HideCustomUI(IGetUI_ID i,UnityAction callBack)
    {
        if (showImagesDic.ContainsKey(i.UI_ID))
        {
            showImagesDic[i.UI_ID].Hide(callBack);
            showImagesDic.Remove(i.UI_ID);
        }
        else
            Debug.Log("不存在此气泡_" + i.UI_ID);
    }

    /// <summary>
    /// 改变UI的图片
    /// </summary>
    /// <param name="isMatch">送餐是否送对</param>
    private void ChangeCustomUI(IGetUI_ID i ,bool isMatch)
    {
        if(showImagesDic.ContainsKey(i.UI_ID))
        {
            showImagesDic[i.UI_ID].ChangeSprite(isMatch);
        }
    }

    private void OnDestroy()
    {
        MyEventSystem.Instance.RemoveEventListener<int>(EventKeyNameData.UI_UpdateMoney, UpdateMoney);
        MyEventSystem.Instance.RemoveEventListener<int>(EventKeyNameData.UI_UpdateOverNum, UpdateOverNum);
        MyEventSystem.Instance.RemoveEventListener<int>(EventKeyNameData.UI_UpdateToDoNUm, UpdatetoDoNum);
        MyEventSystem.Instance.RemoveEventListener<Transform, DishType>(EventKeyNameData.UI_ShowImage, ShowCustomUI);
        MyEventSystem.Instance.RemoveEventListener<IGetUI_ID, UnityAction>(EventKeyNameData.UI_DelImage, HideCustomUI);
        MyEventSystem.Instance.RemoveEventListener<IGetUI_ID, bool>(EventKeyNameData.UI_ChangeImage, ChangeCustomUI);
        MyEventSystem.Instance.RemoveEventListener<Vector2, Sprite>(EventKeyNameData.UI_ShowEffector, ShowLianJiEffector);
    }
}
