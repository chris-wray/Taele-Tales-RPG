using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    public string difficultyLevel;
    public GameObject difficultyPanel;
    private SFXManager sfxMan;

    // Start is called before the first frame update
    void Start()
    {
        difficultyLevel = "Normal";
        sfxMan = FindObjectOfType<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void changeDifficulty(string newDifficulty)
    {
        difficultyLevel = newDifficulty;
        sfxMan.ButtonClick.Play();
    }
}
