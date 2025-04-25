using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePanel : BasePanel<ChoosePanel>
{
    public UIButton btnClose;
    public UIButton btnLeft;
    public UIButton btnRight;
    public UIButton btnStart;

    public Transform heroPos;

    public List<GameObject> hpObjs;
    public List<GameObject> speedObjs;
    public List<GameObject> volumeObjs;

    private GameObject _nowHeroObj;

    public Camera uiCamera;

    protected override void Init()
    {
        btnStart.onClick.Add(new EventDelegate(() => { SceneManager.LoadScene("GameScene"); }));

        btnLeft.onClick.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.nowHeroIndex--;
            if (GameDataMgr.Instance.nowHeroIndex < 0)
            {
                GameDataMgr.Instance.nowHeroIndex = GameDataMgr.Instance.roleData.roleList.Count - 1;
            }

            ChangeNowHero();
        }));

        btnRight.onClick.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.nowHeroIndex++;
            if (GameDataMgr.Instance.nowHeroIndex > GameDataMgr.Instance.roleData.roleList.Count - 1)
            {
                GameDataMgr.Instance.nowHeroIndex = 0;
            }

            ChangeNowHero();
        }));

        btnClose.onClick.Add(new EventDelegate(() =>
        {
            BeginPanel.Instance.ShowMe();
            HideMe();
        }));

        HideMe();
    }

    public override void ShowMe()
    {
        base.ShowMe();
        GameDataMgr.Instance.nowHeroIndex = 0;
        ChangeNowHero();
    }

    public override void HideMe()
    {
        base.HideMe();
        DestroyObj();
    }

    private void ChangeNowHero()
    {
        RoleInfo roleInfo = GameDataMgr.Instance.GetNowSelHeroInfo();
        DestroyObj();

        _nowHeroObj = Instantiate(Resources.Load<GameObject>(roleInfo.resName), heroPos, false);
        _nowHeroObj.transform.localPosition = Vector3.zero;
        _nowHeroObj.transform.localRotation = Quaternion.identity;
        _nowHeroObj.transform.localScale = Vector3.one * roleInfo.scale;

        _nowHeroObj.layer = LayerMask.NameToLayer("UI");

        for (int i = 0; i < 10; i++)
        {
            hpObjs[i].SetActive(i < roleInfo.hp);
            speedObjs[i].SetActive(i < roleInfo.speed);
            volumeObjs[i].SetActive(i < roleInfo.volume);
        }
    }

    private void DestroyObj()
    {
        if (_nowHeroObj)
        {
            Destroy(_nowHeroObj);
            _nowHeroObj = null;
        }
    }

    private float _time;
    private bool _isSel;

    void Update()
    {
        _time += Time.deltaTime;
        heroPos.Translate(Vector3.up * (MathF.Sin(_time) * 0.0001f), Space.World);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(uiCamera.ScreenPointToRay(Input.mousePosition), 1000, 1 << LayerMask.NameToLayer("UI")))
            {
                _isSel = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isSel = false;
        }

        if (Input.GetMouseButton(0) && _isSel)
        {
            heroPos.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 20, Vector3.up);
        }
    }
}