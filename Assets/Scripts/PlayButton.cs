using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    Animator anim;
    GameManager gameManager;
    bool Pressed = false;

    DigitalScreen digitalScreen;

    public List<GameObject> keySounds;

    private void Start()
    {
        anim = GetComponent<Animator>();
        gameManager = FindAnyObjectByType<GameManager>();
        digitalScreen = FindAnyObjectByType<DigitalScreen>();
    }

    public void PlayButtonAction()
    {
        Pressed = !Pressed;

        if (!Pressed)
        {
            gameManager.coPilotandNotAllow = true;
            digitalScreen.DigitalScreenAction();
            anim.SetTrigger("Reset");
            gameManager.Stopper();
            foreach (GameObject sound in keySounds) { sound.GetComponent<AudioSource>().Stop(); }
        }
        if (Pressed)
        {
            gameManager.coPilotandNotAllow = false;
            digitalScreen.DigitalScreenAction();
            anim.SetTrigger("Pressed");
            gameManager.ResetCopilotValues("Hos Geldiniz");
        }
    }
}
