using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysScript : MonoBehaviour
{
    AudioSource keyTone;
    Animator animator;
    public Material originalMat, clickMat;
    public Material trueClickMat, wrongClickMat;
    GameManager gameManager;
    [SerializeField] string noteName;

    private void Start()
    {
        keyTone = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Update()
    {
       keyTone.volume = gameManager.VolumeChanger();
    }

    public void KeyActions()
    {
        keyTone.Play();
        animator.SetTrigger("Clicking");
        FindAnyObjectByType<DigitalScreen>().ScreenTextChanger(noteName);
    }

    public void MaterialChanger()
    {
        gameObject.GetComponent<MeshRenderer>().material = clickMat;
        Invoke(nameof(Original), gameManager.toneSpeed);
    }

    public void ClickMaterial(bool isTrueClick)
    {
        if (isTrueClick)
        {
            gameObject.GetComponent<MeshRenderer>().material = trueClickMat;
        }
        if (!isTrueClick)
        {
            gameObject.GetComponent<MeshRenderer>().material = wrongClickMat;
        }
        Invoke(nameof(Original), gameManager.toneSpeed);
    }

    public void Original()
    {
        gameObject.GetComponent<MeshRenderer>().material = originalMat;
    }
}
