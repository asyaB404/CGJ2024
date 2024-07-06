using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class GamePanel : BasePanel<GamePanel>
{

    private Dictionary<int,CusUIInfo> showImagesDic = new Dictionary<int, CusUIInfo>();
    protected override void Awake()
    {
        base.Awake();


    }

    private void Start()
    {
        ShowCustomUI(null, DishType.Dish1);
        
    }

    private void Update()
    {
      
    }


   

    public void UpdateMoney(int money)
    {
        GetControl<Text>("MoneyText").text = money.ToString();
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


    public void ShowCustomUI(Transform trans,DishType type)
    {
        GameObject obj = Resources.Load<GameObject>("Prefabs/CustomerUI");
        obj = Instantiate(obj);
        obj.transform.SetParent(transform,true);
        obj.GetComponent<CusUIInfo>().Show(trans, type);
    }

    public void HideCustomUI(UnityAction callBack)
    {

    }

    private void OnDestroy()
    {
    }
}
