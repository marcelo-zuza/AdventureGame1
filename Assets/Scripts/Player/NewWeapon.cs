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
    public AudioClip fireSound;
    public GameObject weaponPrefab;
}
