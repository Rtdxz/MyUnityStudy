using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    private AudioSource audioSoure;
    void Awake()
    {
        audioSoure = GetComponent<AudioSource>();
        instance = this;
       
    }
    public void PauseBGM()
    {
        audioSoure.Pause();
    }
   public void ContinueBGM()
    {
        audioSoure.UnPause();
    }
    
}
