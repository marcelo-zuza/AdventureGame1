using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponAnimationController : MonoBehaviour
{
    private Animator weaponAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        StartCoroutine(DelayedInitialization());
    }

    public void FindAnimator()
    {
        weaponAnimator = GetComponentInChildren<Animator>();
        if(weaponAnimator == null)
        {
            Debug.Log("Animator NOT FOUND");
        }else
        {
            Debug.Log("animator FOUND");
        }
    }

    IEnumerator DelayedInitialization()
    {

        yield return new WaitForSeconds(1f);
        FindAnimator();
        yield break;
    }

    public void OnWeaponChanged()
    {
        FindAnimator();
    }

    //public void GetAnimator()
    //{
    //    weaponAnimator = GetComponentInChildren<Animator>();
    //}

    // Update is called once per frame
    public void PlayShootAnimation()
    {
        if(weaponAnimator != null)
        {
            weaponAnimator.SetTrigger("Shoot");
        }
        else
        {
            Debug.Log("Weapon animator FOUND");
        }
    }

    public void PlayReloadAnimation()
    {
        if(weaponAnimator != null)
        {
            weaponAnimator.SetTrigger("Reload");
        }
    }

    public void PlayEquipAnimation()
    {
        if (weaponAnimator != null)
        {
            weaponAnimator.SetTrigger("Equip");
        }
    }

    public bool IsAnimationPlaying(string animationName)
    {
        if (weaponAnimator != null)
        {
            return weaponAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
        }
        return false;
    }
}
