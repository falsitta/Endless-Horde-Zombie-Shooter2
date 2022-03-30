using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon", order = 2)]
public class WeaponScriptable : EquippableScriptable
{
    public WeaponStats weaponStats;

    public override void UseItem(PlayerController playerController)
    {
        if (equipped)
        {
            //unequip from inventory here
            //remove from controller here too
        }
        else
        { 
            //invoke OnWeaponEquipped from player here for inventory
            //equip weapon from weapon holder on playercontroller
        }

        base.UseItem(playerController);
    }
}
