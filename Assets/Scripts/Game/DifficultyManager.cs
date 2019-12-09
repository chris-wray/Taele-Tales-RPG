using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    public string difficultyLevel;
    public GameObject difficultyPanel;
    private SFXManager sfxMan;

    private MusicController musicCon;

    // Start is called before the first frame update
    void Start()
    {
        difficultyLevel = "Normal";
        sfxMan = FindObjectOfType<SFXManager>();
        musicCon = FindObjectOfType<MusicController>();
    }

    void Update()
    {
        if (difficultyLevel == "Normal")
        {
            //Debug.Log("Switching music to normal pitch");
            musicCon.musicTracks[musicCon.currentTrack].pitch = 1;
        }
        else if (difficultyLevel == "Peaceful")
        {
            //Debug.Log("Switching music to peaceful pitch");
            musicCon.musicTracks[musicCon.currentTrack].pitch = 0.7F;
        }
        else if (difficultyLevel == "Hard")
        {
            //Debug.Log("Switching music to hard pitch");
            musicCon.musicTracks[musicCon.currentTrack].pitch = 1.3F;
        }
    }

    public void changeDifficulty(string newDifficulty)
    {
        difficultyLevel = newDifficulty;
        sfxMan.ButtonClick.Play();
    }
}
