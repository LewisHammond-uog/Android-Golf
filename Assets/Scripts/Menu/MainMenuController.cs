using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    [SerializeField]
    private string level1SceneName, optionsMenuSceneName ,howToPlayScene;

    [SerializeField]
    private GameObject musicControllerObject;

    private void Start()
    {
        if(FindObjectOfType<MusicController>() == null)
        {
            Instantiate(musicControllerObject);
        }
    }

    /// <summary>
    /// Starts the game playing
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(level1SceneName);
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene(optionsMenuSceneName);
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene(howToPlayScene);
    }

    /// <summary>
    /// Quits the game
    /// </summary>
	public void QuitGame()
    {
        Application.Quit();
    }
}
