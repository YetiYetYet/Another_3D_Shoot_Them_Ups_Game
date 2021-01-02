﻿using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    public List<GameObject> wawes;
    [ReadOnly] public GameObject currentWawe;

    private void OnEnable()
    {
        Enemy.NextState += ToNextState;
    }

    private void OnDisable()
    {
        Enemy.NextState -= ToNextState;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWawe = wawes[0];
        StopCurrentWawe();
        if (wawes.Count != Enemy.Instance.numberState)
        {
            Debug.LogWarning("Wawes number is different to number of enemy state, last wawes will be taken");
        }
        for (int i = 0; i < wawes.Count; i++)
        {
            wawes[i].gameObject.SetActive(false);
        }
        StartCurrentWawe();
    }

    public void ToNextState(Enemy enemy)
    {
        StopCurrentWawe();
        if (enemy.currentState-1 > wawes.Count)
        {
            return;
        }
        currentWawe = wawes[enemy.currentState-1];
        StartCurrentWawe();

    }

    public void StartCurrentWawe()
    {
        currentWawe.SetActive(true);
    }

    public void StopCurrentWawe()
    {
        currentWawe.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
