using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    #region singleton

    private static Enemy _instance;
    public static Enemy Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion
    public int hitPointsPerStates = 100;
    public int numberState = 3;
    [ReadOnly]
    public int actualLife;
    [ReadOnly]
    public int currentState;

    public Animator animator;
    public bool lookAtPlayer = true;
    public GameObject lookedObject;

    public static event Action<Enemy> NextState;
    public static event Action<Enemy> Die;


    // Start is called before the first frame update
    void Start()
    {
        actualLife = hitPointsPerStates;
        currentState = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookAtPlayer)
        {
            transform.LookAt(lookedObject.transform);
        }
    }

    public void TakeDamage(int damage)
    {
        actualLife -= damage;
        if (actualLife <= 0)
        {
            if (currentState < numberState)
            {
                currentState++;
                actualLife = hitPointsPerStates;
                animator.SetInteger("State", currentState);
                animator.SetTrigger("Hurt");
                
                NextState?.Invoke(this);
            }
            else
            {
                Die?.Invoke(this);
                Debug.Log("Victory");
            }
        }
    }
}
