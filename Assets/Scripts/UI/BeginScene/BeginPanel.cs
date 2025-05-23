using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginPanel : BasePanel<BeginPanel>
{
    public UIButton btnStart;
    public UIButton btnRank;
    public UIButton btnSetting;
    public UIButton btnQuit;

    protected override void Init()
    {
        btnStart.onClick.Add(new EventDelegate(() =>
        {
            ChoosePanel.Instance.ShowMe();

            HideMe();
        }));

        btnRank.onClick.Add(new EventDelegate(() => { RankPanel.Instance.ShowMe(); }));

        btnSetting.onClick.Add(new EventDelegate(() => { SettingPanel.Instance.ShowMe(); }));

        btnQuit.onClick.Add(new EventDelegate(() => { Application.Quit(); }));
    }
}