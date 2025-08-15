using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioClip click_snd, flip_snd, correct_snd, inCorrect_snd, win_snd;
    [SerializeField]
    private AudioSource audioSource;

    void OnEnable()
    {
        AudioEvents.ButtonClickSound += PlayButtonClick;
        AudioEvents.CardFlipSound += PlayCardFlip;
        AudioEvents.CorrectMatchSound += PlayMatchCorrect;
        AudioEvents.InCorrectmatchSound += PlayMatchIncorrect;
        AudioEvents.WinSound += PlayMatchWin;
    }

    void OnDisable()
    {
        AudioEvents.ButtonClickSound -= PlayButtonClick;
        AudioEvents.CardFlipSound -= PlayCardFlip;
        AudioEvents.CorrectMatchSound -= PlayMatchCorrect;
        AudioEvents.InCorrectmatchSound -= PlayMatchIncorrect;
        AudioEvents.WinSound -= PlayMatchWin;
    }


    void PlayButtonClick() => PlaySound(click_snd);
    void PlayCardFlip() => PlaySound(flip_snd);
    void PlayMatchCorrect() => PlaySound(correct_snd);
    void PlayMatchIncorrect() => PlaySound(inCorrect_snd);
    void PlayMatchWin() => PlaySound(win_snd);


    void PlaySound(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}



public static class AudioEvents
{
    public static Action ButtonClickSound;
    public static Action CardFlipSound;
    public static Action CorrectMatchSound;
    public static Action InCorrectmatchSound;
    public static Action WinSound;
}
