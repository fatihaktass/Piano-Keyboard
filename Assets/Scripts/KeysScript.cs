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

    private void Start()
    {
        keyTone = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        gameManager = FindAnyObjectByType<GameManager>();
    }
    public void KeyActions()
    {
        keyTone.Play();
        animator.SetTrigger("Clicking");
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
