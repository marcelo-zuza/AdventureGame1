using System.Collections;
using UnityEngine;

public class PistolMuzzleEffect : MonoBehaviour
{
    public GameObject muzzleEffectPrefab;
    public float muzzleFlashDuration = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TriggerMuzzleEffect()
    {
        StartCoroutine(ShowMuzzleFlash());
    }

    private IEnumerator ShowMuzzleFlash()
    {
        if(muzzleEffectPrefab != null)
        {
            muzzleEffectPrefab.SetActive(true);
            
        }
        yield return new WaitForSeconds(muzzleFlashDuration);

        if(muzzleEffectPrefab != null)
        {
            muzzleEffectPrefab.SetActive(false);
        }
    }
}
