using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    private MusicController mc;

    public int newTrack;

    public bool switchOnStart;

    // Start is called before the first frame update
    void Start()
    {
        mc = FindObjectOfType<MusicController>();

        if (switchOnStart)
        {
            mc.SwitchTrack(newTrack);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            mc.SwitchTrack(newTrack);
            gameObject.SetActive(false);
        }
    }
}
