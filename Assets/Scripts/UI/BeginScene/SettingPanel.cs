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
            
        }));
                
        sldSound.onChange.Add(new EventDelegate(() =>
        {
            
        }));
                
        togMusic.onChange.Add(new EventDelegate(() =>
        {
            
        }));
                        
        togSound.onChange.Add(new EventDelegate(() =>
        {
            
        }));
        
        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
    }

    public override void HideMe()
    {
        base.HideMe();
    }
}