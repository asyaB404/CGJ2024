using UnityEngine;
using DG.Tweening;


public class Customer : MonoBehaviour
{
    //顾客需要的菜品类型
    public DishType WantType { get; private set; }

    //送餐成功给予的积分
    private int _score;

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private SpriteRenderer sr1;

    public void InitForRandom()
    {
        sr.color = new Color(1, 1, 1, 0);
        sr.DOFade(1, 0.25f);
        sr1.color = new Color(1, 1, 1, 0);
        sr1.DOFade(1, 0.25f);
        WantType = Utils.GetRandomEnumValue<DishType>();
        sr1.sprite = DishMgr.Instance.DishIcons1[(int)WantType];
        _score = 1;
    }

    /// <summary>
    /// 送餐完的回调函数,需要传入一个是否是正确的
    /// </summary>
    /// <param name="isRight"></param>
    public void SendCallBack(bool isRight)
    {
        ComboMgr.Instance.Combing = isRight;
        if (isRight)
        {
            Player.Instance.Score += _score * ComboMgr.Instance.Combo;
            sr1.sprite = DishMgr.Instance.DishIcons1[^1];
            MusicMgr.Instance.PlaySound("a1");
        }
        else
        {
            Player.Instance.Health -= 10;
            sr1.sprite = DishMgr.Instance.DishIcons1[^2];
        }
    }

    public void DeSpawn()
    {
        sr1.DOFade(0, 0.5f);
        sr.DOFade(0, 0.35f).OnComplete(() => { Destroy(gameObject); });
    }
}