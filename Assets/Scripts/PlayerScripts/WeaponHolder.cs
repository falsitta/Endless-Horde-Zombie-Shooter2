using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponToSpawn"), SerializeField]
    GameObject weaponToSpawn;

    public PlayerController playerController;
    Animator playerAnimator;
    public Sprite crossHairImage;
    public WeaponComponent equippedWeapon;
    public WeaponAmmoUI weaponAmmoUI;

    GameObject spawnedWeapon;



    [SerializeField]
    GameObject weaponSocketLocation;
    [SerializeField]
    Transform gripIKSocketLocation;

    bool firingPressed = false;

    public readonly int isFiringHash = Animator.StringToHash("IsFiring");
    public readonly int isReloadingHash = Animator.StringToHash("IsReloading");
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimator = GetComponent<Animator>();
        //spawnedWeapon = Instantiate(weaponToSpawn, weaponSocketLocation.transform.position, weaponSocketLocation.transform.rotation, weaponSocketLocation.transform);
        //EquipWeapon()
        //equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        //equippedWeapon.Initialize(this);
        //PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);
        //gripIKSocketLocation = equippedWeapon.gripLocation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (equippedWeapon)
        {
            if (!playerController.isReloading)
            {
                playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gripIKSocketLocation.transform.position);

            }
        }

    }
    public void OnFire(InputValue value)
    {

        firingPressed = value.isPressed;
        if (!equippedWeapon)
            return;
        if (firingPressed)
        {
            StartFiring();
        }
        else
        {
            StopFiring();
        }
    }

    void StartFiring()
    {

        if (equippedWeapon.weaponStats.bulletsInClip <= 0)
        {
            StartReloading();
            return;
        }

        playerAnimator.SetBool(isFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiringWeapon();

    }

    void StopFiring()
    {

        playerAnimator.SetBool(isFiringHash, false);
        playerController.isFiring = false;
        equippedWeapon.StopFiringWeapon();
    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        if (!equippedWeapon)
            return;
        StartReloading();
    }

    public void StartReloading()
    {
        if (playerController.isFiring)
        {
            StopFiring();
        }
        if (equippedWeapon.weaponStats.totalBullets <= 0)
        {
            return;
        }

        //refill ammo here
        equippedWeapon.StartReloading();
        playerController.isReloading = true;
        playerAnimator.SetBool(isReloadingHash, true);        

        InvokeRepeating(nameof(StopReloading), 0, 0.1f);
    }

    public void StopReloading()
    {
        if (playerAnimator.GetBool(isReloadingHash)) return;

        playerController.isReloading = false;
        playerAnimator.SetBool(isReloadingHash, false);
        equippedWeapon.StopReloading();
        CancelInvoke(nameof(StopReloading));

        if (firingPressed)
        {
            StartFiring();
        }
    }

    public void EquipWeapon(WeaponScriptable weaponScriptable)
    {
        if (!weaponScriptable) return;

        spawnedWeapon = Instantiate(weaponScriptable.itemPrefab, weaponSocketLocation.transform.position, weaponSocketLocation.transform.rotation, weaponSocketLocation.transform);
        if (!spawnedWeapon) return;


        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        
        if (!equippedWeapon) return;
        equippedWeapon.Initialize(this, weaponScriptable);
        PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);

        gripIKSocketLocation = equippedWeapon.gripLocation;

        weaponAmmoUI.OnWeaponEquipped(equippedWeapon);
        
    }

    public void UnEquipWeapon()
    {
        if (!equippedWeapon) return;

        Destroy(equippedWeapon.gameObject);
        equippedWeapon = null;
    }
}
