using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapons/Weapon")]
public class NewWeapon : ScriptableObject
{
    public string weaponName;
    public int maxAmmo;
    public int currentAmmo;
    public int reserveAmmo;
    public float reloadTime;
    public float fireRate;
    public float damage;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public GameObject weaponPrefab;
    public GameObject muzzleFlashPrefab;
    public Transform muzzleFlashPrefabPosition;
}
