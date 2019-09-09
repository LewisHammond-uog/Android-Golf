using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayMenu : MonoBehaviour {

    [SerializeField]
    private string mainMenuSceneName;

    /// <summary>
    /// Return to main menu
    /// </summary>
	public void ReturnToMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
