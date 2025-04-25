using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    public UIButton btnClose;

    public UIScrollView svList;


    private List<RankItem> rankItemList = new List<RankItem>();

    protected override void Init()
    {
        btnClose.onClick.Add(new EventDelegate(() => { HideMe(); }));

        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();

        List<RankInfo> rankList = GameDataMgr.Instance.rankData.rankList;

        for (int i = 0; i < rankList.Count; i++)
        {
            if (rankItemList.Count > i)
            {
                rankItemList[i].InitInfo(i + 1, rankList[i].name, rankList[i].time);
            }
            else
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("UI/RankItem"), svList.transform, false);

                obj.transform.localPosition = new Vector3(0, 113 - 53 * i, 0);
                
                RankItem item = obj.GetComponent<RankItem>();
                item.InitInfo(i + 1, rankList[i].name, rankList[i].time);
                rankItemList.Add(item);
            }
        }
    }
}