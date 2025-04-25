using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankItem : MonoBehaviour
{
    public UILabel lblRank;
    public UILabel lblName;
    public UILabel lblTime;

    public void InitInfo(int rank, string name, int time)
    {
        lblRank.text = rank.ToString();
        lblName.text = name;

        var hours = time / 3600;
        var minutes = (time % 3600) / 60;
        var seconds = time % 60;

        lblTime.text = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
    }
}