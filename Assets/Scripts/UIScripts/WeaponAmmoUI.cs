using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponAmmoUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI weaponNameText;
    [SerializeField] TextMeshProUGUI currentBulletCountText;
    [SerializeField] TextMeshProUGUI totalBulletCountText;

    [SerializeField] WeaponComponent weaponComponent;

    /// <summary>
    /// set up events for onweaponequipped to handle the weapon component we grab
    /// </summary>

    private void Start()
    {
        PlayerEvents.OnWeaponEquipped += OnWeaponEquipped;
    }

    private void OnDestroy()
    {
        
        PlayerEvents.OnWeaponEquipped -= OnWeaponEquipped;
        
    }

    public void OnWeaponEquipped(WeaponComponent _weaponComponent)
    {
        weaponComponent = _weaponComponent;
    }

    // Update is called once per frame
    void Update()
    {
        if (!weaponComponent)
            return;

        weaponNameText.text = weaponComponent.weaponStats.weaponName;
        currentBulletCountText.text = weaponComponent.weaponStats.bulletsInClip.ToString();
        totalBulletCountText.text = weaponComponent.weaponHolder.playerController.inventory.FindItem("AK-47").amountValue.ToString();
    }
}
