using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    public UIButton btnBack;
    public UILabel lblTime;

    public List<GameObject> hpObjs;

    public float nowTime = 0;

    protected override void Init()
    {
        btnBack.onClick.Add(new EventDelegate(() => { QuitPanel.Instance.ShowMe(); }));
    }

    public void ChangeHp(int hp)
    {
        for (int i = 0; i < hpObjs.Count; i++)
        {
            hpObjs[i].SetActive(i < hp);
        }
    }

    void Update()
    {
        nowTime += Time.deltaTime;

        var hours = (int)(nowTime / 3600);
        var minutes = (int)((nowTime % 3600) / 60);
        var seconds = (int)(nowTime % 60);

        lblTime.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
    }
}