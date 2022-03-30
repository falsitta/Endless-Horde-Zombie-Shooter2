using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquippableScriptable : ItemScript
{
    private bool isEquipped = false;
    public bool equipped
    {

        get => isEquipped;
        set
        {
            isEquipped = value;
            OnEquipStatusChange?.Invoke();
        }

    }

    public delegate void EquipStatusChange();
    public event EquipStatusChange OnEquipStatusChange;

    public override void UseItem(PlayerController playerController)
    {
        isEquipped = !isEquipped;
    }

}
