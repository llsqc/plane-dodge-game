using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EPosType
{
    TopLeft,
    Top,
    TopRight,

    Left,
    Right,

    BottomLeft,
    Bottom,
    BottomRight
}

public class FireObject : MonoBehaviour
{
    public EPosType posType;

    private FireInfo _fireInfo;
    private int nowNum;
    private float nowCd;
    private float nowDelay;

    private BulletInfo nowBulletInfo;

    private float changeAngle;

    private Vector3 screenPos;
    private Vector3 initDir;
    private Vector3 nowDir;

    private int lastScreenWidth;
    private int lastScreenHeight;

    void Start()
    {
        UpdatePos();
    }

    void Update()
    {
        if (lastScreenWidth != Screen.width || lastScreenHeight != Screen.height)
        {
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
            UpdatePos();
        }

        ResetFireInfo();

        UpdateFire();
    }

    private void UpdatePos()
    {
        screenPos.z = 200;
        switch (posType)
        {
            case EPosType.TopLeft:
                screenPos.x = 0;
                screenPos.y = Screen.height;
                initDir = Vector3.right;
                break;
            case EPosType.Top:
                screenPos.x = Screen.width / 2;
                screenPos.y = Screen.height;
                initDir = Vector3.right;
                break;
            case EPosType.TopRight:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height;
                initDir = Vector3.left;
                break;
            case EPosType.Left:
                screenPos.x = 0;
                screenPos.y = Screen.height / 2;
                initDir = Vector3.right;
                break;
            case EPosType.Right:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height / 2;
                initDir = Vector3.left;
                break;
            case EPosType.BottomLeft:
                screenPos.x = 0;
                screenPos.y = 0;
                initDir = Vector3.right;
                break;
            case EPosType.Bottom:
                screenPos.x = Screen.width / 2;
                screenPos.y = 0;
                initDir = Vector3.right;
                break;
            case EPosType.BottomRight:
                screenPos.x = Screen.width;
                screenPos.y = 0;
                initDir = Vector3.left;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        transform.position = Camera.main.ScreenToWorldPoint(screenPos);
    }

    private void ResetFireInfo()
    {
        if (nowCd != 0 && nowNum != 0)
        {
            return;
        }

        if (_fireInfo != null)
        {
            nowDelay -= Time.deltaTime;
            if (nowDelay > 0)
            {
                return;
            }
        }

        List<FireInfo> list = GameDataMgr.Instance.fireData.fireInfoList;
        _fireInfo = list[Random.Range(0, list.Count)];
        nowNum = _fireInfo.num;
        nowCd = _fireInfo.cd;
        nowDelay = _fireInfo.delay;

        string[] strs = _fireInfo.ids.Split(',');
        int beginID = int.Parse(strs[0]);
        int endID = int.Parse(strs[1]);
        int randomBulletID = Random.Range(beginID, endID + 1);
        nowBulletInfo = GameDataMgr.Instance.bulletData.bulletInfoList[randomBulletID - 1];

        if (_fireInfo.type == 2)
        {
            switch (posType)
            {
                case EPosType.TopLeft:
                case EPosType.TopRight:
                case EPosType.BottomLeft:
                case EPosType.BottomRight:
                    changeAngle = 90f / (nowNum + 1);
                    break;

                case EPosType.Top:
                case EPosType.Left:
                case EPosType.Right:
                case EPosType.Bottom:
                    changeAngle = 180f / (nowNum + 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void UpdateFire()
    {
        if (nowCd == 0 && nowNum == 0)
        {
            return;
        }

        nowCd -= Time.deltaTime;
        if (nowCd > 0)
        {
            return;
        }

        GameObject bullet;
        BulletObject bulletObject;
        switch (_fireInfo.type)
        {
            case 1:
                bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName), transform.position,
                    Quaternion.LookRotation(PlayerObject.Instance.transform.position - transform.position));
                bulletObject = bullet.AddComponent<BulletObject>();
                bulletObject.InitInfo(nowBulletInfo);

                nowNum--;
                nowCd = nowNum == 0 ? 0 : _fireInfo.cd;
                break;
            case 2:
                if (nowCd == 0)
                {
                    for (var i = 0; i < nowNum; i++)
                    {
                        bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                        bulletObject = bullet.AddComponent<BulletObject>();
                        bulletObject.InitInfo(nowBulletInfo);

                        bullet.transform.position = transform.position;
                        nowDir = Quaternion.AngleAxis(changeAngle * i, Vector3.up) * initDir;
                        bullet.transform.rotation = Quaternion.LookRotation(nowDir);
                    }

                    nowCd = nowNum = 0;
                }
                else
                {
                    bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                    bulletObject = bullet.AddComponent<BulletObject>();
                    bulletObject.InitInfo(nowBulletInfo);

                    bullet.transform.position = transform.position;
                    nowDir = Quaternion.AngleAxis(changeAngle * (_fireInfo.num - nowNum), Vector3.up) * initDir;
                    bullet.transform.rotation = Quaternion.LookRotation(nowDir);

                    nowNum--;
                    nowCd = nowNum == 0 ? 0 : _fireInfo.cd;
                }

                break;
        }
    }
}