using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class GamePanel : BasePanel<GamePanel>
{
    //��������
    public Transform test;
    //
    private Dictionary<int,CusUIInfo> showImagesDic = new Dictionary<int, CusUIInfo>();
    private int id = 0;
    //������Чʱ��
    [Tooltip("������Ч����ʱ��")]
    public float stayTime = 0.5f;
    //������Ч�Ŵ���
    [Tooltip("����ͼ�Ŵ���")]
    public float Multip = 1.5f;
    [Tooltip("�Ŵ��ٶ�")]
    public float speed = 1;
    private float curTime;
    private bool isShowEffector;
    private bool isStaye;
    private Vector3 scale;
    private Image effector;
    private void Start()
    {
        scale = Vector3.one* Multip;
        //�¼�����
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
        //��������
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
            ShowLianJiEffector(new Vector2(Screen.width/4, Screen.height/4),Resources.Load<Sprite>("UI/ʷ��ķ������"));
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
    /// ������Ч��ʾ
    /// </summary>
    /// <param name="pos">λ��,һ��Ļ����Ϊԭ��</param>
    /// <param name="sprite">ͼƬ</param>
    private void ShowLianJiEffector(Vector2 pos ,Sprite sprite)
    {
        effector = GetControl<Image>("Effector");
        if(effector == null)
        {
            Debug.LogError("�ؼ���Effector ������,���������Ϊ Effector ��Image���");
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
    /// �������������ʾ
    /// </summary>
    /// <param name="num">�������</param>
    private void UpdateOverNum(int num)
    {
        GetControl<Text>("OverNumText").text = num.ToString();
    }
    /// <summary>
    /// ���´�ȡ������
    /// </summary>
    /// <param name="num">��ȡ������</param>
    private void UpdatetoDoNum(int num)
    {
        GetControl<Text>("ToDoNumText").text = num.ToString();
    }

    /// <summary>
    /// ��ʾ����
    /// </summary>
    /// <param name="trans">�˿͵�Tranform</param>
    /// <param name="type">Dish����</param>
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
            Debug.LogError("��ȷ���������ʹ���ˡ���" + typeof(IGetUI_ID).Name + "�����ӿ�");
        else
            getId.UI_ID = info.id;
        info.Show(trans, type);
        if (!showImagesDic.ContainsKey(info.id))
            showImagesDic.Add(info.id, info);
        else
            Debug.LogError(id + "�Ľ��Ѵ���");
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <param name="i">ID�ӿ�</param>
    /// <param name="callBack">�˿���ʧ�ص�����</param>
    private void HideCustomUI(IGetUI_ID i,UnityAction callBack)
    {
        if (showImagesDic.ContainsKey(i.UI_ID))
        {
            showImagesDic[i.UI_ID].Hide(callBack);
            showImagesDic.Remove(i.UI_ID);
        }
        else
            Debug.Log("�����ڴ�����_" + i.UI_ID);
    }

    /// <summary>
    /// �ı�UI��ͼƬ
    /// </summary>
    /// <param name="isMatch">�Ͳ��Ƿ��Ͷ�</param>
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
