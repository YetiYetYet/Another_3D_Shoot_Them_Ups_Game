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
    
    public Skill[] skills;

    public void PlaySkill(string skillName, Transform origin)
    {
        foreach (var skill in skills)
        {
            if (skill.name == skillName)
            {
                Instantiate(skill.effect, skill.effectTransform);
            }
        }
    }
    
}
