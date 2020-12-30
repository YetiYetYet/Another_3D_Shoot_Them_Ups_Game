using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MonoBehaviour
{
    public float speed = 0.1f;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, speed, 0);
            animator.SetBool("IsMoving", true);
            animator.SetBool("MovingToLeft", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, -speed, 0);
            animator.SetBool("IsMoving", true);
            animator.SetBool("MovingToLeft", false);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Enemy.Instance.TakeDamage(10);
        }
    }
}
