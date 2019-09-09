using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

    [SerializeField]
    private string mainMenuSceneName;

    [Header("Options Slider")]
    [SerializeField]
    private Slider musicSlider, sensitivitySlider;

    /// <summary>
    /// Return to main menu
    /// </summary>
	public void ReturnToMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ApplySettings()
    {
        //Store results in static class and get them to update
        SavedOptions.MusicVolume = Mathf.RoundToInt(musicSlider.value);
        SavedOptions.InputSensitivity = sensitivitySlider.value;

        SavedOptions.UpdatePlayingMusicVolume();
    }
}
