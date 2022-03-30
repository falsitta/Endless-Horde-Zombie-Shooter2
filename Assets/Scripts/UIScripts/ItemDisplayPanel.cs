using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayPanel : MonoBehaviour
{
    [SerializeField] private GameObject ItemSlotPrefab;

    private RectTransform RectTransform;

    void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        WipeChildren();
    }

    public void PopulatePanel(List<ItemScript> itemList)
    {
        WipeChildren();

        foreach(ItemScript item in itemList)
        {
            IconSlot icon = Instantiate(ItemSlotPrefab, RectTransform).GetComponent<IconSlot>();
            icon.Initialize(item);
        }
    }

    private void WipeChildren()
    {
        foreach(RectTransform child in RectTransform)
        {
            Destroy(child.gameObject);
        }
        RectTransform.DetachChildren();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
