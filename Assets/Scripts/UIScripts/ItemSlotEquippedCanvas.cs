using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotEquippedCanvas : MonoBehaviour
{
    EquippableScriptable Equipable;
    [SerializeField] private Image EnabledImage;

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
        if (!(item is EquippableScriptable eqItem)) return;

        Equipable = eqItem;
        ShowWidget();
        Equipable.OnEquipStatusChange += OnEquipmentChange;
        OnEquipmentChange();

    }

    private void OnEquipmentChange()
    {
        EnabledImage.gameObject.SetActive(Equipable.equipped);
    }

    private void OnDisable()
    {
        if (Equipable) 
            Equipable.OnEquipStatusChange -= OnEquipmentChange;
    }
}
