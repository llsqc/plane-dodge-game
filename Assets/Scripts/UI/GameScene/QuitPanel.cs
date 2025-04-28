using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPanel : BasePanel<QuitPanel>
{
    public UIButton btnClose;
    public UIButton btnSure;

    protected override void Init()
    {
        btnSure.onClick.Add(new EventDelegate(() => { SceneManager.LoadScene("BeginScene"); }));

        btnClose.onClick.Add(new EventDelegate(() => { HideMe(); }));

        HideMe();
    }
}