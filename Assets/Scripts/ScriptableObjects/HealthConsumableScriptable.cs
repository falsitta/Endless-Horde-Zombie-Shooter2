using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthPotion", menuName = "Items/HealthPotion", order = 1)]
public class HealthConsumableScriptable : ConsumableScriptable
{
    public override void UseItem(PlayerController playerController)
    {
        if (playerController.healthComponent.CurrentHealth >= playerController.healthComponent.MaxHealth) return;

        playerController.healthComponent.HealPlayer(effect);

        base.UseItem(playerController);
    }
}
