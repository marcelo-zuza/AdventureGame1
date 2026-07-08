using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float playerHealth = 100;
    public TextMeshProUGUI healthText;
    public GameObject damageEffect;
    private bool isDead = false;
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
        RestartGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water")) Die();
    }

    void RestartGame()
    {
        if(isDead && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(0);
        }
    }

    

    public void Die()
    {
        Debug.Log("You're Dead");
        Time.timeScale = 0;
        if (deathPanel != null) deathPanel.SetActive(true);
        isDead = true;

    }
}
