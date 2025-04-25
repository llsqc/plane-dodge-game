using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : BasePanel<SettingPanel>
{
    public UIButton btnClose;

    public UISlider sldMusic;
    public UISlider sldSound;

    public UIToggle togMusic;
    public UIToggle togSound;

    protected override void Init()
    {
        btnClose.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
        }));
        
        sldMusic.onChange.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.SetMusicVolume(sldMusic.value);
        }));
                
        sldSound.onChange.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.SetSoundVolume(sldSound.value);
        }));
                
        togMusic.onChange.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.SetMusicIsOpen(togMusic.value);
        }));
                        
        togSound.onChange.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.SetSoundIsOpen(togSound.value);
        }));
        
        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        MusicData musicData = GameDataMgr.Instance.musicData;
        togMusic.value = musicData.isMusicOpen;
        togSound.value = musicData.isSoundOpen;
        sldMusic.value = musicData.musicVolume;
        sldSound.value = musicData.soundVolume;
    }

    public override void HideMe()
    {
        base.HideMe();
        GameDataMgr.Instance.SaveMusicData();
    }
}