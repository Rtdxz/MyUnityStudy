using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;
    [SerializeField]
    private AudioClip jumpAudio, colletAudio, hurtAudio,trampolineAudio;
    private AudioSource jumpSource, collectSource, hurtSource, trampolineSource;
    void Awake()
    {
        instance = this;
        jumpSource = gameObject.AddComponent<AudioSource>();
        collectSource= gameObject.AddComponent<AudioSource>();
        hurtSource= gameObject.AddComponent<AudioSource>();
        trampolineSource= gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio(string audioName)
    {
        switch (audioName)
        { case "jump":
                audioSource.clip = jumpAudio;
                break;

          case "collect":
                audioSource.clip = colletAudio;
                break;

            case "hurt":
                audioSource.clip = hurtAudio;
                break;
            case "trampoline":
                audioSource.clip = trampolineAudio;
                break;
            default:
                break;
        }
        
        audioSource.Play();
    }
    public void StopAudio()
    {
        audioSource.Stop();
    }
    public void ContinueAudio()
    {
        audioSource.Play();
    }
    public void PlayJumpAudio()
    {
        instance.jumpSource.clip = jumpAudio;
        instance.jumpSource.Play();
    }
    public void PlayHurtAudio()
    {
        instance.hurtSource.clip = hurtAudio;
        instance.hurtSource.Play();
    }
    public void PlayCollectAudio()
    {
        instance.collectSource.clip = colletAudio;
        instance.collectSource.Play();
    }
    public void PlayTrampolineAudio()
    {
        instance.trampolineSource.clip = trampolineAudio;
        instance.trampolineSource.Play();
    }
}

