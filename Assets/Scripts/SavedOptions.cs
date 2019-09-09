using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SavedOptions{

    public delegate void MusicEvent(float volume);
    public static event MusicEvent UpdateMusicVolume;

    private static float musicVolume = 1;
    public static float MusicVolume
    {
        get { return musicVolume; }
        set
        {
            musicVolume = value / (float)100;
        }
    }

    private static float inputSensitivity = 2f;
    public static float InputSensitivity
    {
        get { return inputSensitivity; }
        set
        {
            if(value < 0f)
            {
                inputSensitivity = 0f;
            }
            else
            {
                inputSensitivity = value;
            }
        }
    }

    /// <summary>
    /// Update the current playing music to the current
    /// music volume
    /// </summary>
    public static void UpdatePlayingMusicVolume()
    {
        //Try and find a music controller
        if(UpdateMusicVolume != null)
        {
            UpdateMusicVolume(musicVolume);
        }
    }
	
}
