using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 100;
    public TextMeshProUGUI healthText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( playerHealth <= 0)
        {
            Die();
        }
        if(healthText != null) healthText.text = playerHealth.ToString();
    }

    

    void Die()
    {
        Debug.Log("You're Dead");
    }
}
