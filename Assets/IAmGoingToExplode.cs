using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IAmGoingToExplode : MonoBehaviour
{
    public GameObject END;
    public GameObject GameScreen;
    public void ENDME()
    {
        GameScreen.SetActive(false);
        print("YES");
        END.SetActive(true);
    }

    
    public GameObject waveCountText;
    public void Wave(int waveNum)
    {
        waveCountText.GetComponent<TextMeshProUGUI>().text = "Wave: " + waveNum;
    }
}
