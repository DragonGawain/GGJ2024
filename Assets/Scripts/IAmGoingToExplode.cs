using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IAmGoingToExplode : MonoBehaviour
{
    public GameObject END;
    public GameObject GameScreen;
    [SerializeField]
    private AudioClip gameOverMusicClip;
    public void ENDME()
    {
        AudioSource audioSource = GameObject.FindGameObjectWithTag("MusicObject").GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.clip = gameOverMusicClip;
        audioSource.loop = false;
        audioSource.Play();
        GameScreen.SetActive(false);
        
        END.SetActive(true);
    }

    
    public GameObject waveCountText;
    public void Wave(int waveNum)
    {
        waveCountText.GetComponent<TextMeshProUGUI>().text = "Wave: " + waveNum;
    }
}
