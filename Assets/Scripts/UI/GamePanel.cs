using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : PanelBase
{
    public static GamePanel Instance { get; private set; }
    public GameObject sideOBJ;
    public Slider health;
    public Text score;
    public Text waiter;
    public Button menu;

    public List<Sprite> sprites;

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    protected override void Init()
    {
        menu.onClick.AddListener(() => { UIManager.Instance.ShowPanel<StopPanel>(); });
    }

    protected override void Update()
    {
        base.Update();
        int custCount = CustomerMgr.Instance.Count;
        waiter.text = "待取餐人数：" + custCount;
        if (custCount > 20) sideOBJ.GetComponent<Image>().sprite = sprites[1];
        else sideOBJ.GetComponent<Image>().sprite = sprites[0];
    }
}