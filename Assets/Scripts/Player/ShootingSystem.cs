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
    [SerializeField] private float muzzleFlashDuration = 0.5f;
    private NewWeapon weapon;


    // Muzzle Flash Effect
    private float nextFireTime = 0f; // fire rate controller time

    public void Shoot()
    {
        weapon = weaponManager.GetCurrentWeapon();

        if (Time.time < nextFireTime)
        {
            Debug.Log($"Waiting for fire rate cooldown... Next fire time: {nextFireTime}, Current time: {Time.time}");
            return;
        }

        if (weapon.currentAmmo > 0)
        {
            weapon.currentAmmo--;
            if (audioSource != null) audioSource.PlayOneShot(weapon.fireSound);
            Debug.Log("Bang");

            // Log the fire rate for debugging
            Debug.Log($"Weapon fire rate: {weapon.fireRate}");

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

            Debug.Log($"Next fire time set to: {nextFireTime}");

            // Activate the Muzzle Flash
            StartCoroutine(ShowMuzzleFlash());
        }
        else
        {
            Debug.Log("No AMMO");
        }
    }
    private IEnumerator ShowMuzzleFlash()
    {
        //if (weapon != null && weapon.muzzleFlashPrefab != null && weapon.muzzleFlashPrefabPosition != null)
        //{
        //    Debug.Log("Instantiating Muzzle Flash...");
        //    GameObject muzzleFlash = Instantiate(weapon.muzzleFlashPrefab, weapon.muzzleFlashPrefabPosition.position, weapon.muzzleFlashPrefabPosition.rotation);

        //    // Log the instantiated object for debugging
        //    Debug.Log($"Muzzle Flash instantiated at position: {weapon.muzzleFlashPrefabPosition.position}");

        //    yield return new WaitForSeconds(muzzleFlashDuration);

        //    Debug.Log("Destroying Muzzle Flash...");
        //    Destroy(muzzleFlash);
        //}
        if(weapon != null)
        {
            if(weapon.muzzleFlashPrefab != null)
            {
                if(weapon.muzzleFlashPrefabPosition != null)
                {
                    Debug.Log("Instantiating Muzzle Flash...");
                    GameObject muzzleFlash = Instantiate(weapon.muzzleFlashPrefab, weapon.muzzleFlashPrefabPosition.position, weapon.muzzleFlashPrefabPosition.rotation);

                    // Log the instantiated object for debugging
                    Debug.Log($"Muzzle Flash instantiated at position: {weapon.muzzleFlashPrefabPosition.position}");

                    yield return new WaitForSeconds(muzzleFlashDuration);

                    Debug.Log("Destroying Muzzle Flash...");
                    Destroy(muzzleFlash);
                }else
                {
                    Debug.Log("muzzle flash position not found");
                    yield return null;
                }
            }
            else
            {
                Debug.Log("Weapom muzzle flash prefab not found");
                yield return null;
            }
        }else
        {
            Debug.LogWarning("weapon not found");
            yield return null;
        }
    }


}
