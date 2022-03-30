using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class InventoryCanvas : GameHUDWidget
{
    private ItemDisplayPanel ItemDisplayPanel;
    private List<CategorySelectButton> CategoryButtons;
    private PlayerController PlayerController;

    private void Awake()
    {
        PlayerController = FindObjectOfType<PlayerController>();
        CategoryButtons = GetComponentsInChildren<CategorySelectButton>().ToList();
        ItemDisplayPanel = GetComponentInChildren<ItemDisplayPanel>();
        foreach(CategorySelectButton button in CategoryButtons)
        {
            button.Initialize(this);
        }
    }

    private void OnEnable()
    {
        if (!PlayerController || !PlayerController.inventory) return;
        if (PlayerController.inventory.GetItemCount() <= 0) return;

        ItemDisplayPanel.PopulatePanel(PlayerController.inventory.GetItemsOfCategory(ItemCategory.None));
    }

    public void SelectCategory(ItemCategory category)
    {
        if (!PlayerController || !PlayerController.inventory) return;
        if (PlayerController.inventory.GetItemCount() <= 0) return;

        ItemDisplayPanel.PopulatePanel(PlayerController.inventory.GetItemsOfCategory(category));
    }
}
