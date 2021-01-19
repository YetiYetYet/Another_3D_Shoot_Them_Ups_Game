using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleController : MonoBehaviour
{
    public float speed = 0.1f;
    
    public bool isCastingSpell;
    private Animator animator;

    private  AnimatorClipInfo _currentClip;
    // Start is called before the first frame update
    void Start()
    {
        isCastingSpell = false;
        animator = Player.Instance.playerAnimator;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(GameManager.Instance.currentState != GameManager.GameState.Playing) return;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameManager.Instance.currentState == GameManager.GameState.Playing)
                GameManager.Instance.OnPause();
            else if(GameManager.Instance.currentState == GameManager.GameState.Pause)
                GameManager.Instance.ResumeGame();
        }
        
        if(GameManager.Instance.currentState != GameManager.GameState.Playing) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (!Enemy.Instance.isDead)
                CastSpell(0);
        }

        if (Input.GetMouseButtonDown(1))
        {
            CastSpell(1);
        }
        
    }

    private bool CastSpell(int index)
    {
        if (isCastingSpell)
        {
            return false;
        }
        SkillsManager.Instance.LoadSkill(index);
        if (Player.Instance.mana < SkillsManager.Instance.actualSkill.manaCost) return false;
        Player.Instance.mana -= SkillsManager.Instance.actualSkill.manaCost;
        animator.SetInteger("SkillNumber", index+1);
        animator.SetBool("IsMoving", false);
        animator.SetFloat("CastingSpeed", SkillsManager.Instance.actualSkill.castingSpeed);
        StartCoroutine(nameof(CastingSpell));
        return true;
    }

    IEnumerator CastingSpell()
    {
        if (isCastingSpell) yield break;
        isCastingSpell = true;
        
        // Wait until transition is finish (It's a little tricky, there is probably a better option)
        _currentClip =  SkillsManager.Instance.gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0];
        animator.SetTrigger("PlaySkill");
        AnimatorClipInfo animationClipInfo = SkillsManager.Instance.gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0];
        while (!animationClipInfo.clip.name.ToLower().Contains("skill"))
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
