using UnityEngine;

public class Dish : MonoBehaviour
{
    public void Init()
    {
    }

    public void DeSpawn()
    {
        //销毁音效，动画啥的？
        Destroy(gameObject);
    }
}