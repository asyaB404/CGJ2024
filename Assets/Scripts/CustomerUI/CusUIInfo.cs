using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class CusUIInfo : MonoBehaviour
{
    [Header("顾客图片显示位置的偏移量")]
    public float offset;
    [Tooltip("显隐速度")]
    public float fadeSpeed;

    [HideInInspector]
    public int id;

    private bool isShow;
    private bool isHide;

    private Image image;

    private Transform targetTran;

    private Color unit = new Color(0, 0, 0, 1);

    private UnityAction afterHideEvents;
    private void Awake()
    {
        image = GetComponent<Image>();
        image.color = new Color(1, 1, 1, 0);
    }

    private void Update()
    {
        if(isShow && image.color.a < 1)
        {
            image.color += unit * fadeSpeed*Time.deltaTime;
            if(image.color.a >= 1)
            {
                isShow = false;
                image.color = new Color(1,1,1,1);
            }
        }
        else if(isHide && image.color.a > 0)
        {
            image.color -= unit* fadeSpeed*Time.deltaTime;
            if(image.color.a <= 0)
            {
                isHide = false;
                image.color = new Color(1, 1, 1, 0);
                Destroy(this.gameObject);
                afterHideEvents?.Invoke();
            }
        }
        UpdatePos(targetTran);
    }

    public void Show(Transform CustomTran,DishType type)
    {
        isShow = true;
        image.sprite = IntiSprite(type);
        image.color = new Color(1, 1, 1, 0);
        targetTran = CustomTran;
    }

    public void Hide(UnityAction callBack)
    {
        isHide = true;
        image.color = new Color(1, 1, 1, 1);
        afterHideEvents = callBack;
    }

    private void UpdatePos(Transform trans)
    {
        if(trans != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(trans.position);
        }
    }

    private Sprite IntiSprite(DishType type)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("UI/Dish");
        return sprites[(int)type];
    }

    public void ChangeSprite(bool isMatch)
    {
        Sprite[] sprtes = Resources.LoadAll<Sprite>("UI/TorF");
        image.sprite = isMatch ? sprtes[0] : sprtes[1];
    }
}
