using UnityEngine;

public class Dish : MonoBehaviour
{
    [SerializeField] private DishInfo info;

    public void Init(DishInfo info)
    {
        this.info = info;
    }

    public void DeSpawn()
    {
        //销毁音效，动画啥的？
        Destroy(gameObject);
    }
}