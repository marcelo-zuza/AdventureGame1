using UnityEngine;

public class RealoadSystem : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;

    public void Reload()
    {
        var weapon = weaponManager.GetCurrentWeapon();
        if(weapon.reserveAmmo > 0 && weapon.currentAmmo < weapon.maxAmmo)
        {
            int ammoNeeded = weapon.maxAmmo - weapon.currentAmmo;
            int ammoToReload = Mathf.Min(ammoNeeded, weapon.reserveAmmo);

            weapon.currentAmmo += ammoToReload;
            weapon.reserveAmmo -= ammoToReload;

            Debug.Log($"Reloaded {ammoToReload} bullets. Current Ammo: {weapon.currentAmmo}, Reserver Ammo: {weapon.reserveAmmo}");
        } else
        {
            Debug.Log("No reserve Ammo or no need to reload");
        }
    }
}
