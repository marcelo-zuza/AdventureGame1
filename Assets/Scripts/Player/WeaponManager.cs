using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private NewWeapon[] weapons;
    [SerializeField] private Transform weaponPosition;
    private int currentWeaponIndex = 0;
    private GameObject currentWeaponGameObject;

    void Start()
    {
        EquipWeapon(currentWeaponIndex);
    }


    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) return;
        //desativate current weapon
        if(currentWeaponGameObject != null)
        {
            currentWeaponGameObject.SetActive(false);
        }

        // activate new weapon
        currentWeaponGameObject = Instantiate(weapons[index].weaponPrefab, weaponPosition);
        currentWeaponGameObject.SetActive(true);
        currentWeaponIndex = index;

        Debug.Log($"Equiéd Weapon: {weapons[index].weaponName}");
    }

    public void NextWeapon()
    {
        EquipWeapon((currentWeaponIndex + 1) % weapons.Length);
    }

    public void PreviousWeapon()
    {
        EquipWeapon((currentWeaponIndex - 1 + weapons.Length) % weapons.Length);
    }

    public NewWeapon GetCurrentWeapon()
    {
        return weapons[currentWeaponIndex];
    }
}
