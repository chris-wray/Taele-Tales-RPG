using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePanel : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClosingPanel()
    {
        if (Panel != null)
        {
            Panel.SetActive(false);
            Button.SetActive(false);
        }
    }
}
