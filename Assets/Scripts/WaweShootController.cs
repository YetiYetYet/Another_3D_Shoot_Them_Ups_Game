using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WaweShootController : MonoBehaviour
{
    [ReadOnly]
    private List<UbhBaseShot> UbhBaseShotsChild;
    [ReadOnly]
    public int actualShotCtrlIndex;
    private void Start()
    {
        UbhBaseShotsChild = new List<UbhBaseShot>();
        //Debug.Log(gameObject.GetComponentsInChildren<UbhBaseShot>().Length);
        foreach (var ubhShotCtrl in gameObject.GetComponentsInChildren<UbhBaseShot>())
        {
            UbhBaseShotsChild.Add(ubhShotCtrl);
        }

        if (UbhBaseShotsChild.Count == 0)
        {
            Debug.LogError("No Pattern Found in child");
        }

        actualShotCtrlIndex = 0;

        foreach (var ubhBaseShot in UbhBaseShotsChild)
        {
            ubhBaseShot.m_shotFinishedCallbackEvents.AddListener(NextPattern);
        }

        for (int i = 1; i < UbhBaseShotsChild.Count; i++)
        {
            UbhBaseShotsChild[i].gameObject.SetActive(false);
        }
    }

    public void NextPattern()
    {
        //Debug.Log("NextPatternCall");
        UbhBaseShotsChild[actualShotCtrlIndex].gameObject.SetActive(false);
        actualShotCtrlIndex++;
        if (actualShotCtrlIndex > UbhBaseShotsChild.Count-1)
        {
            actualShotCtrlIndex = 0;
        }
        UbhBaseShotsChild[actualShotCtrlIndex].gameObject.SetActive(true);
    }
}
