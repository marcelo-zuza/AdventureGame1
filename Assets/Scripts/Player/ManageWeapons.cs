using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ManageWeapons : MonoBehaviour
{
    [Header("Weapon Management")]
    // types of guns
    private const int PISTOL = 0;
    private const int AK47 = 1;
    private const int M16 = 2;
    [SerializeField] private int totalNumberOfGuns = 3;
    [SerializeField] GameObject[] weaponsGameObjects = new GameObject[3];
    private GameObject currentWeaponGameObject;
    // managing
    private int activeWeapon = PISTOL;
    private bool[] hasWeapon;
    private int[] ammos;
    private int[] reserveAmmos;
    private int[] gunMaxBuletsCapacity;
    private float[] reloadTime;
    private string[] weaponNames;
    private AudioClip[] weaponsFX;

    // Aiming System
    Camera playerCamera;
    Ray rayFromPlayer;
    RaycastHit hit;
    [SerializeField] GameObject sparksOfImpact;

    [Header("Audio")]
    private AudioSource audioSource;
    [SerializeField] AudioClip pistolFx;
    [SerializeField] AudioClip shotgunFX;
    [SerializeField] AudioClip ak47FX;
    [SerializeField] AudioClip m16FX;
    [SerializeField] AudioClip noBulletsFX;

    // Inputs
    [SerializeField] InputActionAsset inputActionAsset;
    private InputAction fireAction;
    private InputAction reloadAction;
    private InputAction nextWeapon;
    private InputAction previousWeapon;

    //Animators
    [SerializeField] private Animator[] gunAnimators;
    private Animator activeAnimator;

    private int currentWeapon;

    //UI
    [SerializeField] private TextMeshProUGUI currentAmmoDisplay;
    [SerializeField] private TextMeshProUGUI reserveAmmoDisplay;
    // Reloading System
    bool canShoot = true;
    private bool isShooting = false; // Control variable to ensure shooting animation runs once

    private void OnEnable()
    {
        if (inputActionAsset != null)
        {
            fireAction = inputActionAsset.FindAction("Shoot", true);
            fireAction.Enable();
            reloadAction = inputActionAsset.FindAction("Reload", true);
            reloadAction.Enable();
            nextWeapon = inputActionAsset.FindAction("NextWeapon", true);
            nextWeapon.Enable();
            previousWeapon = inputActionAsset.FindAction("PreviousWeapon", true);
            previousWeapon.Enable();
        }
    }

    private void OnDisable()
    {
        if (inputActionAsset != null)
        {
            fireAction.Disable();
            reloadAction.Disable();
            nextWeapon.Disable();
            previousWeapon.Disable();
        }
    }


    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        audioSource = GetComponent<AudioSource>();

        // Initiating variables
        ammos = new int[totalNumberOfGuns];
        hasWeapon = new bool[totalNumberOfGuns];
        gunMaxBuletsCapacity = new int[totalNumberOfGuns];
        reserveAmmos = new int[totalNumberOfGuns];
        reloadTime = new float[totalNumberOfGuns];
        weaponNames = new string[totalNumberOfGuns];
        weaponsFX = new AudioClip[totalNumberOfGuns];

        hasWeapon[PISTOL] = true;
        hasWeapon[AK47] = true;
        hasWeapon[M16] = true;

        weaponNames[PISTOL] = "Pistol";
        weaponNames[AK47] = "Ak-47";
        weaponNames[M16] = "M-16";

        ammos[PISTOL] = 12;
        ammos[AK47] = 0;
        ammos[M16] = 0;

        reserveAmmos[PISTOL] = 0;
        reserveAmmos[AK47] = 0;
        reserveAmmos[M16] = 0;

        gunMaxBuletsCapacity[PISTOL] = 12;
        gunMaxBuletsCapacity[AK47] = 32;
        gunMaxBuletsCapacity[M16] = 64;

        weaponsFX[PISTOL] = pistolFx;
        weaponsFX[AK47] = ak47FX;
        weaponsFX[M16] = m16FX;

        currentWeapon = PISTOL;

        currentWeaponGameObject = weaponsGameObjects[currentWeapon];

        activeAnimator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        // Ensure the shooting state is reset if the animation has finished
        if (isShooting && activeAnimator != null && activeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            ResetShootingState();
        }

        // Raytrace
        rayFromPlayer = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        // Shoot & Reload
        ReadInputs();

        UpdateAmmoDisplay();


    }

    void ReadInputs()
    {
        // Shooting Weapon
        if (fireAction.triggered && ammos[currentWeapon] > 0 && canShoot)
        {
            ShootWeapon();
        } else if(fireAction.triggered && ammos[currentWeapon] <= 0 && canShoot && reserveAmmos[currentWeapon] > 0)
        {
            ReloadWeapon();
        } else if(fireAction.triggered && ammos[currentWeapon] <= 0 && canShoot && reserveAmmos[currentWeapon] <= 0)
        {
            if (noBulletsFX != null)
            {
                audioSource.PlayOneShot(noBulletsFX);
            }
            Debug.Log("No Bullets");
        }

        // Reloading
        if (reloadAction.triggered && ammos[currentWeapon] < gunMaxBuletsCapacity[currentWeapon] && reserveAmmos[currentWeapon] > 0)
        {
            ReloadWeapon();
        }

        // Changing Weapon
        if(nextWeapon.triggered)
        {
            ChangeWeapon("+");
        }
        if(previousWeapon.triggered)
        {
            ChangeWeapon("-");
        }
    }

    void ShootWeapon()
    {
        if (isShooting) return; // Prevent multiple triggers

        // Play weapon sound effect
        if (weaponsFX[currentWeapon] != null)
        {
            audioSource.PlayOneShot(weaponsFX[currentWeapon]);
        }
        else
        {
            Debug.Log("Sound fx Not found");
        }

        Debug.Log("Bang");

        // Trigger shooting animation if the animator is ready
        if (activeAnimator != null)
        {
            if (activeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || activeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Ready"))
            {
                activeAnimator.SetTrigger("Shoot");
                isShooting = true; // Set shooting state
            }
            else
            {
                Debug.Log("Animator not ready for shooting animation");
            }
        }
        ResetShootingState();

        // Reduce ammo count
        ammos[currentWeapon]--;

        // Raycast to detect hit
        if (Physics.Raycast(rayFromPlayer, out hit))
        {
            Debug.Log($"Hit: {hit.collider.name}");

            // Instantiate impact sparks if available
            if (sparksOfImpact != null)
            {
                Instantiate(sparksOfImpact, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
        else
        {
            Debug.Log("Missed");
        }
    }

    // Example method to reset shooting state (to be called via Animation Event)
    public void ResetShootingState()
    {
        isShooting = false;
        if (activeAnimator != null)
        {
            activeAnimator.ResetTrigger("Shoot"); // Reset the trigger to avoid looping
        }
    }

    void ReloadWeapon()
    {
        Debug.Log("Reloading");
    }

    void ChangeWeapon(string direction)
    {
        int newWeaponIndex = currentWeapon;
        int step = direction == "+" ? 1 : -1;

        do
        {
            newWeaponIndex = (newWeaponIndex + step + totalNumberOfGuns);
            //
            if(newWeaponIndex > totalNumberOfGuns)
            {
                newWeaponIndex = 0;
            }else if(totalNumberOfGuns < 0)
            {
                newWeaponIndex = totalNumberOfGuns;
            }
            //
        } while (!hasWeapon[newWeaponIndex] && newWeaponIndex != currentWeapon);

        if(newWeaponIndex != currentWeapon)
        {
            currentWeapon = newWeaponIndex;
            UpdateActiveWeapon();
            UpdateAnimator();
        }

    }

    void UpdateActiveWeapon()
    {
        if(currentWeaponGameObject != null)
        {
            currentWeaponGameObject.SetActive(false);
        }

        currentWeaponGameObject = weaponsGameObjects[currentWeapon];
        if(currentWeaponGameObject != null)
        {
            currentWeaponGameObject.SetActive(true);
        }
        Debug.Log($"Current Weapon : {weaponNames[currentWeapon]}");
    }

    void UpdateAnimator()
    {
        activeAnimator = GetComponentInChildren<Animator>();
    }

    void UpdateAmmoDisplay()
    {
        if(currentAmmoDisplay != null)
        {
            currentAmmoDisplay.text = ammos[currentWeapon].ToString() + " / ";
        }

        if(reserveAmmoDisplay != null)
        {
            reserveAmmoDisplay.text = reserveAmmos[currentWeapon].ToString();
        }
    }
}
