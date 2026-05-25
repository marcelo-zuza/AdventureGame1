using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DarknessTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Light directionalLight;
    [SerializeField] private float targetInclination = 0;
    [SerializeField] private float duration = 0.5f;

    private bool isDarkening = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !isDarkening)
        {
            Debug.Log("Darkness trigger activated");
            StartCoroutine(GetDark());

        }
    }

    IEnumerator GetDark()
    {
        isDarkening = true;
        Vector3 currentRotarion = directionalLight.transform.eulerAngles;

        Quaternion startRotation = directionalLight.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(targetInclination, currentRotarion.y, currentRotarion.z);

        float timeElapsed = 0;

        while(timeElapsed < duration)
        {
            directionalLight.transform.rotation = Quaternion.Slerp(startRotation, endRotation, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        directionalLight.transform.rotation = endRotation;
    }
}
