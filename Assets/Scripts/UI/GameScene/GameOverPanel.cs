using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : BasePanel<GameOverPanel>
{
    public UILabel lblTime;
    public UIInput inputName;
    public UIButton btnSure;

    private int _endTime;

    protected override void Init()
    {
        btnSure.onClick.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.AddRankData(inputName.value, _endTime);
            SceneManager.LoadScene("BeginScene");
        }));

        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();

        _endTime = (int)GamePanel.Instance.nowTime;
        lblTime.text = GamePanel.Instance.lblTime.text;
    }
}