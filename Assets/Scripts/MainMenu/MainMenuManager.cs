using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private GameObject instructionsPanel;
    [SerializeField] private GameObject creditsPanel;
    private bool instrctionsOn = false;

    void Start()
    {
        instructionsPanel.gameObject.SetActive(false);
        creditsPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGameButton()
    {
        buttonsPanel.gameObject.SetActive(false);
        instructionsPanel.gameObject.SetActive(true);
    }

    public void CreditsButton()
    {
        buttonsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void StartGameButton()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        creditsPanel.SetActive(false);
        buttonsPanel.SetActive(true);
    }
}
