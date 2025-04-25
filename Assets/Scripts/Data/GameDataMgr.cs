using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr _instance = new GameDataMgr();
    public static GameDataMgr Instance => _instance;

    public MusicData musicData;
    public RankData rankData;
    public RoleData roleData;

    public int nowHeroIndex = 0;

    private GameDataMgr()
    {
        musicData = XmlDataMgr.Instance.LoadData(typeof(MusicData), "MusicData") as MusicData;
        rankData = XmlDataMgr.Instance.LoadData(typeof(RankData), "RankData") as RankData;
        roleData = XmlDataMgr.Instance.LoadData(typeof(RoleData), "RoleData") as RoleData;
    }

    #region Music & Sound

    public void SaveMusicData()
    {
        XmlDataMgr.Instance.SaveData(musicData, "MusicData");
    }

    public void SetMusicIsOpen(bool isOpen)
    {
        musicData.isMusicOpen = isOpen;
        BackMusic.Instance.SetBackMusicIsOpen(isOpen);
    }

    public void SetSoundIsOpen(bool isOpen)
    {
        musicData.isSoundOpen = isOpen;
    }

    public void SetMusicVolume(float volume)
    {
        musicData.musicVolume = volume;
        BackMusic.Instance.SetBackMusicVolume(volume);
    }

    public void SetSoundVolume(float volume)
    {
        musicData.soundVolume = volume;
    }

    #endregion

    #region Rank

    public void AddRankData(string name, int time)
    {
        RankInfo rankInfo = new RankInfo();
        rankInfo.name = name;
        rankInfo.time = time;
        rankData.rankList.Add(rankInfo);

        rankData.rankList.Sort((info, info1) => info1.time.CompareTo(info.time));

        if (rankData.rankList.Count > 20)
        {
            rankData.rankList.RemoveAt(20);
        }

        XmlDataMgr.Instance.SaveData(rankData, "RankData");
    }

    #endregion

    #region RoleData

    public RoleInfo GetNowSelHeroInfo()
    {
        return roleData.roleList[nowHeroIndex];
    }

    #endregion
}