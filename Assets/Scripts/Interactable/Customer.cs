using UnityEngine;


public class Customer : MonoBehaviour,IGetUI_ID
{
    //顾客需要的菜品类型
    public DishType WantType { get; private set; }
    public int UI_ID { get; set; }

    //送餐成功给予的积分
    private int _score;
    
    [SerializeField] private SpriteRenderer sr;

    public void InitForRandom()
    {
        WantType = Utils.GetRandomEnumValue<DishType>();
        //顾客的贴图，到时候得换，测试先用菜品贴图
        sr.sprite = DishMgr.Instance.DishIcons[(int)WantType];
        //可以拓展弄不同的顾客  等级高的顾客加的分多一点或者有让下一个客人慢点来，这里暂时让分数都一样
        _score = 1;
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
            //送错餐的逻辑 暂时没想好 说不定可以让顾客刷新间隔变短或者加一个血条机制啥的
        }
    }

    public void DeSpawn()
    {
        //可以换成客人慢慢消失的逻辑
        Destroy(gameObject);
    }
}