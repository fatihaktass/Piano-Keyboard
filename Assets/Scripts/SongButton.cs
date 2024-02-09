using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongButton : MonoBehaviour
{
    Animator anim;
    bool Pressed;

    GameManager gameManager;
    void Start()
    {
        anim = GetComponent<Animator>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    public void SongButtonAnim()
    {
        Pressed = !Pressed;
        if (Pressed)
        {
            anim.SetTrigger("Pressed");
        }
        if (!Pressed)
        {
            anim.SetTrigger("Reset");
            gameManager.ResetCopilotValues();
        }
    }
}
