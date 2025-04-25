using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMusic : MonoBehaviour
{
    private static BackMusic _instance;
    public static BackMusic Instance => _instance;

    private AudioSource _bkAudioSource;

    void Awake()
    {
        _instance = this;
        _bkAudioSource = gameObject.GetComponent<AudioSource>();

        SetBackMusicIsOpen(GameDataMgr.Instance.musicData.isMusicOpen);
        SetBackMusicVolume(GameDataMgr.Instance.musicData.musicVolume);
    }

    public void SetBackMusicIsOpen(bool isOpen)
    {
        _bkAudioSource.mute = !isOpen;
    }

    public void SetBackMusicVolume(float volume)
    {
        _bkAudioSource.volume = volume;
    }
}