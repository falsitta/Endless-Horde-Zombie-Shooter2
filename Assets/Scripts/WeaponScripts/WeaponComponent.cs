using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{ 
    None, Pistol, MachineGun
}

public enum WeaponFiringPattern
{ 
    SemiAuto, FullAuto, ThreeShotBurst, FiveShotBurst, PumpAction
}

[System.Serializable]
public struct WeaponStats {
    public WeaponType weaponType;
    public WeaponFiringPattern firingPattern;
    public string weaponName;
    public float damage;
    public int bulletsInClip;
    public int clipSize;
    public float fireStartDelay;
    public float fireRate;
    public float fireDistance;
    public bool repeating;
    public LayerMask weaponHitLayers;
    public int totalBullets;
    //bool dumpAmmoOnReload = false;
}

public class WeaponComponent : MonoBehaviour
{
    public Transform gripLocation;
    public WeaponStats weaponStats;

    public WeaponHolder weaponHolder;
    [SerializeField]
    protected ParticleSystem firingEffect;

    public bool isFiring;
    public bool isReloading;


    protected Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void Initialize(WeaponHolder _weaponHolder, WeaponScriptable weaponScriptable)
    {
        weaponHolder = _weaponHolder;

        if (weaponScriptable)
        {
            weaponStats = weaponScriptable.weaponStats;
            weaponStats.totalBullets = weaponHolder.playerController.inventory.FindItem("AK-47").amountValue;
        }
    }

    public virtual void StartFiringWeapon()
    {
        isFiring = true;
        if (weaponStats.repeating)
        {
            //fire weapon
            CancelInvoke(nameof(FireWeapon));
            InvokeRepeating(nameof(FireWeapon), weaponStats.fireStartDelay, weaponStats.fireRate);
        }
        else
        {
            FireWeapon();
        }
    }

    public virtual void StopFiringWeapon()
    {
        isFiring = false;
        CancelInvoke(nameof(FireWeapon));
        if (firingEffect && firingEffect.isPlaying)
        {
            firingEffect.Stop();
        }
    }

    protected virtual void FireWeapon()
    {        
        weaponStats.bulletsInClip--;
        //print(weaponStats.bulletsInClip);
    }

    public virtual void StartReloading()
    {
        isReloading = true;
        ReloadWeapon();
    }
    public virtual void StopReloading()
    {
        isReloading = false;
    }

    //set ammo counts here
    protected virtual void ReloadWeapon()
    {
        //check to see if there is a firing effect and stop it
        if (firingEffect && firingEffect.isPlaying)
        {
            firingEffect.Stop();
        }

        int bulletsToReload = weaponStats.clipSize - weaponStats.totalBullets;
        if (bulletsToReload < 0)
        {
            weaponHolder.playerController.inventory.FindItem("AK-47").amountValue -= (weaponStats.clipSize - weaponStats.bulletsInClip);
            weaponStats.bulletsInClip = weaponStats.clipSize;
        }
        else
        {
            weaponStats.bulletsInClip = weaponStats.totalBullets = weaponHolder.playerController.inventory.FindItem("AK-47").amountValue;
            weaponStats.totalBullets = weaponHolder.playerController.inventory.FindItem("AK-47").amountValue = 0;
        }
    }
    /*
     *   protected virtual void ReloadWeapon()

  {

    // Check to see if there is if there is a firing effect and stop it.

    if (firingEffect && firingEffect.isPlaying)

      firingEffect.Stop();



    int bulletsToFillClip = stats.dumpAmmoOnReload ? stats.clipSize : stats.clipSize - stats.bulletsInClip;

    int bulletsLeftAfter = stats.totalBullets - bulletsToFillClip;



    if (bulletsLeftAfter >= 0)

    {

      if (stats.dumpAmmoOnReload)

        stats.bulletsInClip = 0;

      stats.bulletsInClip += bulletsToFillClip;

      stats.totalBullets -= bulletsToFillClip;

    }

    else

    {

      stats.bulletsInClip += stats.totalBullets;

      stats.totalBullets = 0;

    }

  }
     * */


}
