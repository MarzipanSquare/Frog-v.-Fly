using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Button playButton;
    public Button exitButton;
    
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(LoadGame);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    
    private void ExitGame()
    {
        Application.Quit();
    }
}
