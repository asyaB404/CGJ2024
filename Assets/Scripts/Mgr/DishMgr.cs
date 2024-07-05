using System.Collections.Generic;
using UnityEngine;


public class DishMgr : SingletonMono<DishMgr>
{
    private static readonly int h = Const.H;
    private static readonly int w = Const.W;
    private readonly Queue<Dish>[] _queues = new Queue<Dish>[h];
    [SerializeField] private GameObject dishPrefab;

    public Dish SpawnDish(Vector2 pos)
    {
        GameObject gobj = Instantiate(dishPrefab);
        gobj.transform.position = pos;
        Dish dish = gobj.GetComponent<Dish>();
        
        return dish;
    }

    private void Start()
    {
        for (int i = 0; i < h; i++)
        {
            _queues[i] = new Queue<Dish>();
            for (int j = 0; j < w; j++)
            {
                _queues[i].Enqueue(SpawnDish(new(w - i, j)));
            }
        }
    }
}