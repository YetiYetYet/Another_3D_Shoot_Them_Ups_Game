using System;
using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    #region singleton

    private static SkillsManager _instance;
    public static SkillsManager Instance => _instance;

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
    public Skill actualSkill;

    public Skill[] skills;

    public void Start()
    {
        LoadSkill(0);
    }

    public void LoadSkill(int index)
    {
        actualSkill = skills[index];
        RFX4_EffectEvent rfx4EffectEvent = gameObject.GetComponent<RFX4_EffectEvent>();
        
        rfx4EffectEvent.CharacterEffect = skills[index].CharacterEffect;
        rfx4EffectEvent.CharacterAttachPoint = skills[index].CharacterAttachPoint;
        rfx4EffectEvent.CharacterEffect_DestroyTime = skills[index].CharacterEffect_DestroyTime;

        rfx4EffectEvent.CharacterEffect2 = skills[index].CharacterEffect2;
        rfx4EffectEvent.CharacterAttachPoint2 = skills[index].CharacterAttachPoint2;
        rfx4EffectEvent.CharacterEffect2_DestroyTime = skills[index].CharacterEffect2_DestroyTime;
        
        rfx4EffectEvent.MainEffect = skills[index].MainEffect;
        rfx4EffectEvent.AttachPoint = skills[index].AttachPoint;
        rfx4EffectEvent.OverrideAttachPointToTarget = skills[index].OverrideAttachPointToTarget;
        rfx4EffectEvent.Effect_DestroyTime = skills[index].Effect_DestroyTime;
        
        rfx4EffectEvent.AdditionalEffect = skills[index].AdditionalEffect;
        rfx4EffectEvent.AdditionalEffectAttachPoint = skills[index].AdditionalEffectAttachPoint;
        rfx4EffectEvent.AdditionalEffect_DestroyTime = skills[index].AdditionalEffect_DestroyTime;
    }

    public void LoadSkill(string skillName)
    {
        for (var index = 0; index < skills.Length; index++)
        {
            if (skills[index].name == skillName)
            {
                LoadSkill(index);
                return;
            }
        }
        Debug.LogError("Skill : " + skillName + " not found.");
    }
}