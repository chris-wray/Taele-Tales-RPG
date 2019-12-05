using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject Panel;
    public GameObject CloseButton;
    public GameObject TitleText;
    public GameObject Text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
            CloseButton.SetActive(!isActive);
            TitleText.SetActive(!isActive);
            Text.SetActive(!isActive);
        }
    }
}
