using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private ShootingSystem shootingSystem;
    [SerializeField] private RealoadSystem realoadSystem;
    [SerializeField] private WeaponManager weaponManager;

    [SerializeField] private InputActionAsset inputActionAsset;
    private InputAction fireAction;
    private InputAction reloadAction;
    private InputAction nextWeaponAction;
    private InputAction previousWeaponAction;

    private void OnEnable()
    {
        fireAction = inputActionAsset.FindAction("Shoot", true);
        reloadAction = inputActionAsset.FindAction("Reload", true);
        nextWeaponAction = inputActionAsset.FindAction("NextWeapon", true);
        previousWeaponAction = inputActionAsset.FindAction("PreviousWeapon", true);

        fireAction.Enable();
        reloadAction.Enable();
        nextWeaponAction.Enable();
        previousWeaponAction.Enable();
    }

    private void OnDisable()
    {
        fireAction.Disable();
        reloadAction.Disable();
        nextWeaponAction.Disable();
        previousWeaponAction.Disable();
    }

    


    void Update()
    {
        if(fireAction.triggered)
        {
            shootingSystem.Shoot();
        }

        if(reloadAction.triggered)
        {
            realoadSystem.Reload();
        }

        if(nextWeaponAction.triggered)
        {
            weaponManager.NextWeapon();
        }

        if(previousWeaponAction.triggered)
        {
            weaponManager.PreviousWeapon();
        }
    }
}
