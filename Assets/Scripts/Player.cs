using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region singleton

    private static Player _instance;
    public static Player Instance => _instance;

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

    public int maxHealth = 100;
    public int health = 100;
    public bool isDead = false;
    [Required] public Animator playerAnimator;
    [Required] public Collider playerCollider;

    public static event Action<Player> PlayerDeath; 

    public void TakeDamage(int damage)
    {
        if(isDead) return;
        health -= damage;
        isDead = health <= 0;
        if (isDead)
        {
            playerCollider.enabled = false;
            playerAnimator.SetTrigger("Die");
            PlayerDeath?.Invoke(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
