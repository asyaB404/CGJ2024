using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ComboMgr : SingletonMono<ComboMgr>
{
    [SerializeField] private Sprite[] nums;
    [SerializeField] private GameObject comboNumsPrefabs;
    [SerializeField] private Transform comboParent;
    public int Combo { get; private set; }
    [SerializeField] private bool combing;
    private float _combingTime;

    public bool Combing
    {
        get => combing;
        set
        {
            if (combing != value)
            {
                if (value)
                    comboParent.DOScale(1f, 0.1f);
                else
                    comboParent.DOScale(0f, 0.1f);
            }

            combing = value;
            if (combing == false)
            {
                Combo = 0;
                _combingTime = 0;
            }
            else
            {
                Combo += 1;
                _combingTime = 0.5f;
                UpdateCombo();
            }
        }
    }

    private void Update()
    {
        if (_combingTime > 0)
        {
            _combingTime -= Time.deltaTime;
        }
        else
        {
            Combing = false;
        }
    }

    public void UpdateCombo()
    {
        comboParent.DOKill(true);
        comboParent.DOScale(1.25f, 0.25f).OnComplete(() => { comboParent.DOScale(1f, 0.25f); });
        comboParent.DestroyAllChildren();
        var combostring = Combo.ToString();
        var size = combostring.Length;
        for (var i = 0; i < size; i++)
        {
            var combobj = Instantiate(comboNumsPrefabs, comboParent, false);
            combobj.transform.localPosition = new Vector3(-2.0f - (size - 1 - i), 0);
            combobj.GetComponent<Image>().sprite = nums[int.Parse(combostring[i].ToString())];
        }
    }
}