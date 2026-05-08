using System.Collections;
using System.Runtime.CompilerServices;
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
    [SerializeField] private float muzzleFlashDuration = 0.1f;
    private NewWeapon weapon;


    // Muzzle Flash Effect
    private float nextFireTime = 0f; // fire rate controller time

    public void Shoot()
    {
        //var weapon = weaponManager.GetCurrentWeapon();
        weapon = weaponManager.GetCurrentWeapon();

        if (Time.time < nextFireTime)
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

            // Ativar o Muzzle Flash
            StartCoroutine(ShowMuzzleFlash());


        }
        else
        {
            Debug.Log("No AMMO");
        }
    }
    private IEnumerator ShowMuzzleFlash()
    {
        if(weapon.muzzleFlashPrefab != null || weapon.muzzleFlashPrefabPosition != null)
        {
            GameObject muzzleFlash = Instantiate(weapon.muzzleFlashPrefab, weapon.muzzleFlashPrefabPosition.position, weapon.muzzleFlashPrefabPosition.rotation);
            //muzzleFlash.transform.SetParent(weapon.muzzleFlashPrefabPosition);
            yield return new WaitForSeconds(muzzleFlashDuration);
            Destroy(muzzleFlash);
        } else
        {
            yield return new WaitForSeconds(muzzleFlashDuration);
        }
        

    }


}
