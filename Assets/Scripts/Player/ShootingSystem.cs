using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private WeaponManager weaponManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject sparksOfImpact;
    [SerializeField] private InputAction previousWeaponAction;
    [SerializeField] private WeaponAnimationController weaponAnimationController;

    private float nextFireTime = 0f; // fire rate controller time

    public void Shoot()
    {
        var weapon = weaponManager.GetCurrentWeapon();

        if(Time.time < nextFireTime)
        {
            Debug.Log("Waiting for fire rate cooldown...");
        }

        if(weapon.currentAmmo > 0)
        {
            weapon.currentAmmo--;
            if (audioSource != null) audioSource.PlayOneShot(weapon.fireSound);
            Debug.Log("Bang");

            // use of Raycast
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log($"Hit: {hit.collider.name}");
                if (sparksOfImpact != null)
                {
                    Instantiate(sparksOfImpact, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
            else
            {
                Debug.Log("Missed");
            }

            weaponAnimationController.PlayShootAnimation();

            nextFireTime = Time.time + (1f / weapon.fireRate);
                
        }else
        {
            Debug.Log("No AMMO");
        }
    }
}
