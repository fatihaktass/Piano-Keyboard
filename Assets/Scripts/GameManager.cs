using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> singList;
    public List<GameObject> keyList;
    public List<float> keySpeed;
    int keyIndex = 0;
    int playerKeyIndex;
    public bool coPilotandNotAllow;
    bool playerTurn;
    public float toneSpeed;

    public TextMeshProUGUI dText;

    DigitalScreen digitalScreen;

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
                if (hit.collider.CompareTag("Buttons") && !coPilotandNotAllow)
                {
                    StartCoroutine(ToneDelay());
                }

                if (hit.collider.CompareTag("Play"))
                {
                    FindAnyObjectByType<PlayButton>().PlayButtonAction();
                }

                if (hit.collider.CompareTag("Keys") && !coPilotandNotAllow)
                {
                    hit.collider.gameObject.GetComponent<KeysScript>().KeyActions();
                    if (playerTurn && keyList.Count < singList.Count)
                    {
                        keyList.Add(hit.collider.gameObject);
                        ToneSpeedChanger(playerKeyIndex);
                        if (keyList[playerKeyIndex] == singList[playerKeyIndex])
                        {
                            keyList[playerKeyIndex].GetComponent<KeysScript>().ClickMaterial(true);
                        }
                        else
                        {
                            keyList[playerKeyIndex].GetComponent<KeysScript>().ClickMaterial(false);
                            digitalScreen.ScreenTextChanger("YANLIS TUSLAMA");
                            coPilotandNotAllow = true;
                        }

                        playerKeyIndex++;
                    }
                }
            }
        }
    }

    IEnumerator ToneDelay()
    {
        while (keyIndex < singList.Count)
        {
            coPilotandNotAllow = true;
            ToneSpeedChanger(keyIndex);
            singList[keyIndex].GetComponent<KeysScript>().KeyActions();
            singList[keyIndex].GetComponent<KeysScript>().MaterialChanger();
            keyIndex++;
            yield return new WaitForSeconds(toneSpeed);
        }

        if (keyIndex == singList.Count)
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
}
