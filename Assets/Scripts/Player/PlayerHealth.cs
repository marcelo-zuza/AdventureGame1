using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 100;
    public TextMeshProUGUI healthText;
    public GameObject damageEffect;
    public bool isDead = false;
    public GameObject deathPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if( playerHealth <= 0)
        {
            healthText.text = "00";
            Die();
        }
        if(healthText != null) healthText.text = playerHealth.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water")) Die();
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    

    public void Die()
    {
        if (isDead) return;

        Debug.Log("You're Dead");
        Time.timeScale = 0;
        if (deathPanel != null) deathPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        isDead = true;

    }
}
