using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private static PlayerObject _instance;
    public static PlayerObject Instance => _instance;

    public int nowHp;
    public int maxHp;
    public int speed;
    public int roundSpeed;
    private Quaternion _targetQuaternion;
    public bool isDead = false;

    private Vector3 _nowPos;
    private Vector3 _lastPos;

    private void Awake()
    {
        _instance = this;
    }

    public void Dead()
    {
        isDead = true;
        GameOverPanel.Instance.ShowMe();
    }

    public void Wound()
    {
        if (isDead)
        {
            return;
        }

        nowHp -= 1;
        GamePanel.Instance.ChangeHp(nowHp);

        if (nowHp <= 0)
        {
            Dead();
        }
    }

    private float _hValue;
    private float _vValue;

    void Update()
    {
        if (isDead)
        {
            return;
        }

        _hValue = Input.GetAxisRaw("Horizontal");
        _vValue = Input.GetAxisRaw("Vertical");

        if (_hValue == 0)
        {
            _targetQuaternion = Quaternion.identity;
        }

        else
        {
            _targetQuaternion = _hValue < 0 ? Quaternion.AngleAxis(20, Vector3.forward) : Quaternion.AngleAxis(-20, Vector3.forward);
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, _targetQuaternion, Time.deltaTime * roundSpeed);

        _lastPos = transform.position;

        transform.Translate(Vector3.forward * (_vValue * speed * Time.deltaTime));
        transform.Translate(Vector3.right * (_hValue * speed * Time.deltaTime), Space.World);

        _nowPos = Camera.main.WorldToScreenPoint(transform.position);

        if (_nowPos.x <= 0 || _nowPos.x >= Screen.width)
        {
            transform.position = new Vector3(_lastPos.x, transform.position.y, transform.position.z);
        }

        if (_nowPos.y <= 0 || _nowPos.y >= Screen.height)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, _lastPos.z);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo, 1000, 1 << LayerMask.NameToLayer("Bullet")))
            {
                var bulletObj = hitInfo.transform.GetComponent<BulletObject>();
                bulletObj.Dead();
            }
        }
    }
}