using UnityEngine;

public enum DishType
{
    Dish1,
    Dish2,
    Dish3
}

public class Dish : MonoBehaviour
{
    public DishType Type { get; private set; }
    [SerializeField] private SpriteRenderer sr;

    public void InitForRandom()
    {
        Type = Utils.GetRandomEnumValue<DishType>();
        sr.sprite = DishMgr.Instance.DishIcons[(int)Type];
    }

    public void DeSpawn()
    {
        //销毁音效，动画啥的？
        Destroy(gameObject);
    }
}