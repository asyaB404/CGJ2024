using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class DishMgr : SingletonMono<DishMgr>
{
    private const int H = Const.H;
    private const int w = Const.W;
    private readonly Queue<Dish>[] _queues = new Queue<Dish>[H];
    [SerializeField] private GameObject dishPrefab;
    [SerializeField] private Sprite[] dishIcons;
    public Sprite[] DishIcons => dishIcons;

    public Dish SpawnDish(Vector2 pos)
    {
        GameObject gobj = Instantiate(dishPrefab);
        gobj.transform.position = pos;
        Dish dish = gobj.GetComponent<Dish>();
        dish.InitForRandom();
        return dish;
    }


    public Dish GetDish(int y)
    {
        var queue = _queues[y];
        if (y < 0 || y > Const.H - 1)
        {
            Debug.LogError("y不能超过地图高度");
            return null;
        }

        if (Player.Instance.DishInHand)
        {
            //手上已经有的逻辑
            return null;
        }

        Dish res = queue.Dequeue();
        queue.Enqueue(SpawnDish(new(-1, y)));
        foreach (var dish in queue)
        {
            Transform dishTransform = dish.transform;
            dishTransform.DOKill(true);
            dishTransform.DOLocalMove(dishTransform.position + new Vector3(1, 0, 0), 0.5f);
        }

        return res;
    }

    private void Start()
    {
        for (int i = 0; i < H; i++)
        {
            _queues[i] = new Queue<Dish>();
            for (int j = 0; j < w; j++)
            {
                _queues[i].Enqueue(SpawnDish(new(w - i, j)));
            }
        }
    }
}