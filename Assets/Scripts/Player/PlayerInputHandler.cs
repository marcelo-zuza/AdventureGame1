using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField] private ShootingSystem shootingSystem;
    [SerializeField] private RealoadSystem realoadSystem;
    [SerializeField] private WeaponManager weaponManager;
    //[SerializeField] private WeaponAnimationController weaponAnimationController;

    [SerializeField] private InputActionAsset inputActionAsset;
    private InputAction fireAction;
    private InputAction reloadAction;
    private InputAction nextWeaponAction;
    private InputAction previousWeaponAction;

    PlayerHealth playerHealth;

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


    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
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

        if(playerHealth != null)
        {
            if(playerHealth.isDead)
            {
                if (fireAction.triggered) playerHealth.RestartGame();
            }
        }

    }
}
