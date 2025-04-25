using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr _instance = new GameDataMgr();
    public static GameDataMgr Instance => _instance;

    public MusicData musicData;

    private GameDataMgr()
    {
        musicData = XmlDataMgr.Instance.LoadData(typeof(MusicData), "MusicData") as MusicData;
    }

    public void SaveMusicData()
    {
        XmlDataMgr.Instance.SaveData(musicData, "MusicData");
    }

    public void SetMusicIsOpen(bool isOpen)
    {
        musicData.isMusicOpen = isOpen;
    }

    public void SetSoundIsOpen(bool isOpen)
    {
        musicData.isSoundOpen = isOpen;
    }

    public void SetMusicVolume(float volume)
    {
        musicData.musicVolume = volume;
    }

    public void SetSoundVolume(float volume)
    {
        musicData.soundVolume = volume;
    }
}