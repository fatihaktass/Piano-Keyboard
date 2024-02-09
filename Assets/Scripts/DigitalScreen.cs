using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DigitalScreen : MonoBehaviour
{
    [SerializeField] Material openMat, closedMat;
    [SerializeField] TextMeshProUGUI screenText, volumeText;
    [SerializeField] Animator screenAnim;
    [SerializeField] GameObject screenObj;

    bool openedScreen;

    private void Start()
    {
        screenAnim = GetComponent<Animator>();
    }

    public void DigitalScreenAction()
    {
        openedScreen = !openedScreen;

        if (openedScreen)
        {
            screenText.gameObject.SetActive(true);
            volumeText.gameObject.SetActive(true); 
            screenObj.GetComponent<MeshRenderer>().material = openMat;
            screenAnim.SetTrigger("Open");
        }
        if (!openedScreen)
        {
            screenText.gameObject.SetActive(false);
            volumeText.gameObject.SetActive(false);
            screenObj.GetComponent<MeshRenderer>().material = closedMat;
            screenAnim.SetTrigger("Closed");
        }
    }

    public void ScreenTextChanger(string Message)
    {
        screenText.text = Message;
    }

    public void VolumeTextChanger(string Volume)
    {
        volumeText.text = Volume;
    }
}
