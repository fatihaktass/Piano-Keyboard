using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> songList;
    List<GameObject> keyList;
    public int keyIndex = 0;
    int playerKeyIndex;
    float vol = .5f;
    public bool coPilotandNotAllow;
    bool playerTurn;
    bool songPress;
    public float toneSpeed;
    [SerializeField] ParticleSystem[] fireworks;

    public bool stopCopilot;
    public TextMeshProUGUI dText;

    DigitalScreen digitalScreen;

    public AudioSource onePressButton;

    private void Start()
    {
        coPilotandNotAllow = true;
        digitalScreen = FindAnyObjectByType<DigitalScreen>();
        keyList = new List<GameObject>();
    }

    void Update()
    {
        MouseInput();
    }

    void MouseInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Play"))
                {
                    FindAnyObjectByType<PlayButton>().PlayButtonAction();
                }

                if (hit.collider.CompareTag("VolumeUp"))
                {
                    vol += .1f;
                    VolumeChanger();

                    if (!coPilotandNotAllow)
                        onePressButton.Play();
                }

                if (hit.collider.CompareTag("VolumeDown"))
                {
                    vol -= .1f;
                    VolumeChanger();

                    if(!coPilotandNotAllow)
                        onePressButton.Play();
                }

                if (hit.collider.CompareTag("SongButton") && !coPilotandNotAllow)
                {
                    songPress = !songPress;
                    if (songPress)
                    {
                        FindAnyObjectByType<PlayButton>().CloseAllKeySound();
                        StartCoroutine(ToneDelay()); 
                        digitalScreen.ScreenTextChanger("HABABAM");
                    }
                    if (!songPress)
                    {
                        ResetCopilotValues("Serbest!");
                    }
                    
                }

                if (hit.collider.CompareTag("Keys") && !coPilotandNotAllow)
                {
                    hit.collider.gameObject.GetComponent<KeysScript>().KeyActions();
                    if (playerTurn && keyList.Count < songList.Count)
                    {
                        keyList.Add(hit.collider.gameObject);
                        ToneSpeedChanger(playerKeyIndex);
                        if (keyList[playerKeyIndex] == songList[playerKeyIndex])
                        {
                            keyList[playerKeyIndex].GetComponent<KeysScript>().ClickMaterial(true);
                        }
                        else
                        {
                            keyList[playerKeyIndex].GetComponent<KeysScript>().ClickMaterial(false);
                            digitalScreen.ScreenTextChanger("Yanlis Tuslama");
                            coPilotandNotAllow = true;
                            Invoke("ResetDelay", 3f);
                        }

                        if (keyList.SequenceEqual(songList))
                        {
                            coPilotandNotAllow = true;
                            digitalScreen.ScreenTextChanger("Tebrikler");
                            foreach (ParticleSystem effects in fireworks) { effects.Play(); }
                            Invoke("ResetDelay", 3f);
                        }
                       
                        playerKeyIndex++;
                    }
                }
            }
        }
    }

    public void Stopper()
    {
        stopCopilot = true;
        StopCoroutine(ToneDelay());
    }

    IEnumerator ToneDelay()
    {
        while (keyIndex < songList.Count && !stopCopilot)
        {
            coPilotandNotAllow = true;
            ToneSpeedChanger(keyIndex);
            songList[keyIndex].GetComponent<KeysScript>().KeyActions();
            songList[keyIndex].GetComponent<KeysScript>().MaterialChanger();
            keyIndex++;
            yield return new WaitForSeconds(toneSpeed);
        }

        if (keyIndex == songList.Count)
        {
            coPilotandNotAllow = false;
            dText.text = "SIRA SIZDE";
            playerTurn = true;
        }
    }

    void ToneSpeedChanger(int index)
    {
        switch (index)
        {
            case 2:
                toneSpeed = 0.3f;
                break;
            case 3:
                toneSpeed = 0.3f;
                break;
            case 4:
                toneSpeed = 0.3f;
                break;
            case 5:
                toneSpeed = 0.3f;
                break;
            default:
                toneSpeed = 1f;
                break;
        }

    }

    public float VolumeChanger()
    {
        if (vol > 1)
            vol = 1;

        if (vol < 0)
            vol = 0;

        onePressButton.volume = vol;

        digitalScreen.VolumeTextChanger(Mathf.Round(vol * 10).ToString());
        return vol;
    }

    void ResetDelay()
    {
        ResetCopilotValues("Serbest");
    }

    public void ResetCopilotValues(string Message)
    {
        keyIndex = 0;
        playerKeyIndex = 0;
        keyList.Clear();
        playerTurn = false;
        songPress = false;
        coPilotandNotAllow = false;
        stopCopilot = false;
        foreach (ParticleSystem effects in fireworks) { effects.Stop(); }
        digitalScreen.ScreenTextChanger(Message);
    }
}
