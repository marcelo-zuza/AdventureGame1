using TMPro;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private NewWeapon[] weapons;
    [SerializeField] private Transform weaponPosition;
    [SerializeField] private WeaponAnimationController weaponAnimationController;
    private int currentWeaponIndex = 0;
    private GameObject currentWeaponGameObject;
    // UI
    [SerializeField] private TextMeshProUGUI currentAmmoDisplay;
    [SerializeField] private TextMeshProUGUI reserveAmmoDisplay;

    void Start()
    {
        EquipWeapon(currentWeaponIndex);
    }

    private void Update()
    {
        UpdateAmmoDisplay();
    }

    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;
        //desativate current weapon
        if(currentWeaponGameObject != null)
        {
            //currentWeaponGameObject.SetActive(false);
            Destroy(currentWeaponGameObject);
        }

        // activate new weapon
        currentWeaponGameObject = Instantiate(weapons[index].weaponPrefab, weaponPosition);
        currentWeaponGameObject.SetActive(true);
        

        currentWeaponIndex = index;

        Debug.Log($"Equiéd Weapon: {weapons[index].weaponName}");
        // refresh getting animation controller
        weaponAnimationController.OnWeaponChanged();
    }

    public void NextWeapon()
    {
        EquipWeapon((currentWeaponIndex + 1) % weapons.Length);
        if(weaponAnimationController != null)
        {
            //weaponAnimationController.OnWeaponChanged();
        }else
        {
            Debug.Log("Weapon animator not found");
        }
    }

    public void PreviousWeapon()
    {
        EquipWeapon((currentWeaponIndex - 1 + weapons.Length) % weapons.Length);
        if (weaponAnimationController != null)
        {
            //weaponAnimationController.OnWeaponChanged();
        }
        else
        {
            Debug.Log("Weapon animator not found");
        }
    }

    public NewWeapon GetCurrentWeapon()
    {
        return weapons[currentWeaponIndex];
        
    }

    void UpdateAmmoDisplay()
    {
        if(currentAmmoDisplay != null)
        {
            currentAmmoDisplay.text = weapons[currentWeaponIndex].currentAmmo.ToString() + " / ";
        }

        if(reserveAmmoDisplay != null)
        {
            reserveAmmoDisplay.text = weapons[currentWeaponIndex].reserveAmmo.ToString();
        }
    }
}
