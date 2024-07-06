using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ComboMgr : SingletonMono<ComboMgr>
{
    [SerializeField] private Sprite[] nums;
    [SerializeField] private GameObject comboNumsPrefabs;
    [SerializeField] private Transform comboParent;
    public int Combo { get; private set; }

    [ContextMenuItem("UpdateCombo", "UpdateCombo")] [SerializeField]
    private string combo;

    public void UpdateCombo()
    {
        comboParent.DOKill(true);
        comboParent.DOScale(1.25f, 0.25f).OnComplete(() => { comboParent.DOScale(1f, 0.25f); });
        comboParent.DestroyAllChildren();
        // string combostring = Player.Instance.Combo.ToString();
        string combostring = combo;
        int size = combo.Length;
        for (int i = 0; i < size; i++)
        {
            GameObject combobj = Instantiate(comboNumsPrefabs, comboParent, false);
            combobj.transform.localPosition = new Vector3(-2.0f - (size - 1 - i), 0);
            combobj.GetComponent<Image>().sprite = nums[int.Parse(combostring[i].ToString())];
        }
    }
}