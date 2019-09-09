using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    [SerializeField]
    private AudioClip[] musicClips;

    [SerializeField]
    private AudioSource audioPlayer;

	// Use this for initialization
	void Start () {
        ChangeMusicTrack();

        DontDestroyOnLoad(this);
	}

    private void OnEnable()
    {
        SavedOptions.UpdateMusicVolume += UpdateVolume;
    }
    private void OnDisable()
    {
        SavedOptions.UpdateMusicVolume -= UpdateVolume;
    }


    // Update is called once per frame
    void Update () {

        if (!audioPlayer.isPlaying)
        {
            ChangeMusicTrack();
            audioPlayer.Play();
        }
		
	}

    /// <summary>
    /// Updates the volume of the music (0 to 1)
    /// </summary>
    /// <param name="volume"></param>
    private void UpdateVolume(float volume)
    {
        audioPlayer.volume = volume;
    }
    
    /// <summary>
    /// Changes the currently playing music track
    /// </summary>
    private void ChangeMusicTrack()
    {
        //Choose a random track
        int randomTrackID = Random.Range(0, musicClips.Length);

        //Set that track to play
        audioPlayer.clip = musicClips[randomTrackID];
    }
}
