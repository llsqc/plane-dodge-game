using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    private BulletInfo _bulletInfo;

    private float time;

    public void InitInfo(BulletInfo info)
    {
        _bulletInfo = info;
        Invoke(nameof(DelayDestroy), _bulletInfo.lifeTime);
    }

    private void DelayDestroy()
    {
        Dead();
    }

    public void Dead()
    {
        GameObject effObj = Instantiate(Resources.Load<GameObject>(_bulletInfo.deadEffRes));
        effObj.transform.position = transform.position;
        Destroy(effObj, 0.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerObject player = other.gameObject.GetComponent<PlayerObject>();
            player.Wound();
            Dead();
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * _bulletInfo.forwardSpeed));
        switch (_bulletInfo.type)
        {
            /*
             * 1. 直线移动
             * 2. 曲线移动
             * 3. 右抛物线移动
             * 4. 左抛物线移动
             * 5. 跟踪运动
             */
            case 2:
                time += Time.deltaTime;
                transform.Translate(Vector3.right * (Time.deltaTime * Mathf.Sin(time * _bulletInfo.roundSpeed) * _bulletInfo.rightSpeed));
                break;
            case 3:
                transform.rotation *= Quaternion.AngleAxis(_bulletInfo.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 4:
                transform.rotation *= Quaternion.AngleAxis(-_bulletInfo.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 5:
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    Quaternion.LookRotation(PlayerObject.Instance.transform.position - transform.position),
                    Time.deltaTime * _bulletInfo.roundSpeed);
                break;
            default:
                break;
        }
    }
}