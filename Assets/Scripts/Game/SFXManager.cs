using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource DoorSound;
    public AudioSource PurchaseAccepted;
    public AudioSource PurchaseDenied;
    public AudioSource ButtonClick;
    public AudioSource PlayerHurt;
    public AudioSource SlimeHurt;

    private static bool sfxManExists;

    void Start()
    {
        if (!sfxManExists)
        {
            sfxManExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
