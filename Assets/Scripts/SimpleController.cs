using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MonoBehaviour
{
    public float speed = 0.1f;
    
    public bool isCastingSpell;
    public Animator animator;

    private  AnimatorClipInfo _currentClip;
    // Start is called before the first frame update
    void Start()
    {
        isCastingSpell = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!isCastingSpell)
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
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CastSpell(0);
        }
    }

    private bool CastSpell(int index)
    {
        if (isCastingSpell)
        {
            return false;
        }
        isCastingSpell = true;
        SkillsManager.Instance.LoadSkill(index);
        animator.SetInteger("SkillNumber", index+1);
        animator.SetFloat("CastingSpeed", SkillsManager.Instance.actualSkill.castingSpeed);
        StartCoroutine(nameof(CastingSpell));
        return true;
    }

    IEnumerator CastingSpell()
    {
        isCastingSpell = true;
        
        // Wait until transition is finish (It's a little tricky, there is probably a better option)
        _currentClip =  SkillsManager.Instance.gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0];
        animator.SetTrigger("PlaySkill");
        AnimatorClipInfo animationClipInfo = SkillsManager.Instance.gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0];
        while (_currentClip.clip.name == animationClipInfo.clip.name && !_currentClip.clip.name.ToLower().Contains("skill"))
        {
            animationClipInfo = SkillsManager.Instance.gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0];
            yield return null;
        }

        Debug.Log("Start casting spell " + animationClipInfo.clip.name + " for " +
                  animationClipInfo.clip.length / animator.GetFloat("CastingSpeed") + " seconds");

        yield return new WaitForSeconds(animationClipInfo.clip.length / animator.GetFloat("CastingSpeed"));
        
        //Debug.Log("Casting Spell Finish");
        isCastingSpell = false;
        //Enemy.Instance.TakeDamage(SkillsManager.Instance.actualSkill.damage);
    }
}
