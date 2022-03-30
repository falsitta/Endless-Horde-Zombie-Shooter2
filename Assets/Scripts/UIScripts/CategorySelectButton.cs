using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategorySelectButton : MonoBehaviour
{
    [SerializeField] private ItemCategory Category;

    // References
    private Button SelectButton;
    private InventoryCanvas InventoryWidget;

    // Start is called before the first frame update
    private void Awake()
    {
        SelectButton = GetComponent<Button>();
        SelectButton.onClick.AddListener(OnClick);
    }

    public void Initialize(InventoryCanvas inventoryWidget)
    {
        InventoryWidget = inventoryWidget;
    }

    private void OnClick()
    {
        if (!InventoryWidget) return;
        InventoryWidget.SelectCategory(Category);
    }
}
