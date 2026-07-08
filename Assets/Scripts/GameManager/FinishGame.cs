using UnityEngine;
using UnityEngine.InputSystem;

public class FinishGame : MonoBehaviour
{
    [SerializeField] private GameObject finishGame;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        finishGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            
            if (finishGame != null) finishGame.SetActive(true);
        }
        Cursor.lockState = CursorLockMode.None;
    }
    


}
