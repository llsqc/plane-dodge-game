using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        RoleInfo roleInfo = GameDataMgr.Instance.GetNowSelHeroInfo();

        GameObject heroObj = Instantiate(Resources.Load<GameObject>(roleInfo.resName), Vector3.zero, Quaternion.identity);
        PlayerObject playerObj = heroObj.AddComponent<PlayerObject>();
        playerObj.speed = roleInfo.speed * 20;
        playerObj.maxHp = 10;
        playerObj.nowHp = roleInfo.hp;
        playerObj.roundSpeed = 20;

        GamePanel.Instance.ChangeHp(roleInfo.hp);
    }
}