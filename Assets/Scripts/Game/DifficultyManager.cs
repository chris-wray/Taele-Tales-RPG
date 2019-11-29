using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyManager : MonoBehaviour
{
    public string difficultyLevel;
    public GameObject difficultyPanel;

    // Start is called before the first frame update
    void Start()
    {
        difficultyLevel = "Normal";
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void changeDifficulty(string newDifficulty)
    {
        Debug.Log("Called ChangeDifficulty");
        difficultyLevel = newDifficulty;
    }
}
