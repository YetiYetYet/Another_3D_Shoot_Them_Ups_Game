using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class WaweShootController : MonoBehaviour
{
    [SerializeField, ReadOnly]
    private List<UbhBaseShot> ubhBaseShotsChild;
    [ReadOnly]
    public int actualShotCtrlIndex;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        ubhBaseShotsChild = new List<UbhBaseShot>();
        //Debug.Log(gameObject.GetComponentsInChildren<UbhBaseShot>().Length);
        foreach (var ubhShotCtrl in gameObject.GetComponentsInChildren<UbhBaseShot>())
        {
            ubhBaseShotsChild.Add(ubhShotCtrl);
        }

        if (ubhBaseShotsChild.Count == 0)
        {
            Debug.LogError("No Pattern Found in child");
        }

        actualShotCtrlIndex = 0;

        foreach (var ubhBaseShot in ubhBaseShotsChild)
        {
            ubhBaseShot.m_shotFinishedCallbackEvents.AddListener(NextPattern);
        }

        for (int i = 1; i < ubhBaseShotsChild.Count; i++)
        {
            ubhBaseShotsChild[i].gameObject.SetActive(false);
        }
    }

    public void NextPattern()
    {
        //Debug.Log("NextPatternCall");
        ubhBaseShotsChild[actualShotCtrlIndex].gameObject.SetActive(false);
        actualShotCtrlIndex++;
        if (actualShotCtrlIndex > ubhBaseShotsChild.Count-1)
        {
            actualShotCtrlIndex = 0;
        }
        ubhBaseShotsChild[actualShotCtrlIndex].gameObject.SetActive(true);
    }

    public void StartPattern()
    {
        
        ubhBaseShotsChild[actualShotCtrlIndex].gameObject.SetActive(true);
    }
}
