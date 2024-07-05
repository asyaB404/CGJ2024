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

    public void Init()
    {
        Type = Utils.GetRandomEnumValue<DishType>();
    }

    public void DeSpawn()
    {
        //销毁音效，动画啥的？
        Destroy(gameObject);
    }
}