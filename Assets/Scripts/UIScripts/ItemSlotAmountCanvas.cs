using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemSlotAmountCanvas : MonoBehaviour
{
    [SerializeField] private TMP_Text AmountText;
    private ItemScript Item;

    private void Awake()
    {
        HideWidget();
    }

    public void ShowWidget()
    {
        gameObject.SetActive(true);
    }

    public void HideWidget()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(ItemScript item)
    {
        if (!item.stackable) return;
        Item = item;
        ShowWidget();
        Item.OnAmountChange += OnAmountChange;
        OnAmountChange();
    }

    private void OnAmountChange()
    {
        AmountText.text = Item.amountValue.ToString();
    }

    private void OnDisable()
    {
        if (Item) Item.OnAmountChange -= OnAmountChange;
    }
}
