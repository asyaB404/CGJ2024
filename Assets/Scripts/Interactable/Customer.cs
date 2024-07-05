using UnityEngine;


public class Customer : MonoBehaviour
{
    //顾客需要的菜品类型
    public DishType WantType { get; private set; }
    //送餐成功给予的积分
    private int _score;
    
    [SerializeField] private SpriteRenderer sr;

    public void InitForRandom()
    {
        WantType = Utils.GetRandomEnumValue<DishType>();
        //顾客的贴图，到时候得换，测试先用菜品贴图
        sr.sprite = DishMgr.Instance.DishIcons[(int)WantType];
    }

    /// <summary>
    /// 送餐完的回调函数,需要传入一个是否是正确的
    /// </summary>
    /// <param name="isRight"></param>
    public void SendCallBack(bool isRight)
    {
        if (isRight)
        {
            Player.Instance.Score += _score;
        }
        else
        {
            //送错餐的逻辑 暂时没想好
        }
    }

    public void DeSpawn()
    {
        //销毁音效，动画啥的？
        Destroy(gameObject);
    }
}