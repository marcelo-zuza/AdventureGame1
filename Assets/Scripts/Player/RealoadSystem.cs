using System.Collections;
using UnityEngine;

public class RealoadSystem : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    private WeaponAnimationController weaponAnimationController;
    public float reloadingTime = 0.5f;
    public bool isReloading = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        weaponAnimationController = GetComponent<WeaponAnimationController>();
    }

    public void Reload()
    {
        if (isReloading) return;

        if(weaponManager == null)
        {
            Debug.LogWarning("weaponManager is null");
            return;
        }

        var weapon = weaponManager.GetCurrentWeapon();

        if(weapon == null)
        {
            Debug.LogWarning("Weapon not found");
            return;
        }

        if(weapon.reserveAmmo <= 0 || weapon.currentAmmo >= weapon.maxAmmo)
        {
            Debug.Log("No reserve ammo or no need to reload");
        }

        int ammoNeeded = weapon.maxAmmo - weapon.currentAmmo;
        int ammoToRealod = Mathf.Min(ammoNeeded, weapon.reserveAmmo);
        if(ammoToRealod <= 0)
        {
            Debug.Log("Nothign to reload");
            return;
        }

        isReloading = true;
        if(weaponAnimationController != null)
        {
            weaponAnimationController.PlayReloadAnimation();
        }else
        {
            Debug.LogWarning("ANIMATOR CONTROLLER NOT FOUND");
        }
        
        StartCoroutine(PerformReload(weapon, ammoToRealod));
    }

    private IEnumerator PerformReload(NewWeapon weapon, int ammoToReload)
    {
        yield return new WaitForSeconds(reloadingTime);
        weapon.currentAmmo += ammoToReload;
        weapon.reserveAmmo -= ammoToReload;

        isReloading = false;
    }

    private IEnumerator ReloadingTime()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadingTime);
        isReloading = false;
    }
}
