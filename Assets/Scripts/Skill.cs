using System;
using UnityEngine;

[Serializable]
public class Skill
{
      public string name;
      public int damage;
      public int manaCost;
      public float castingSpeed;

      //public RFX4_EffectEvent rfx4EffectEvent;
      public GameObject CharacterEffect;
      public Transform CharacterAttachPoint;
      public float CharacterEffect_DestroyTime = 10;
      [Space]

      public GameObject CharacterEffect2;
      public Transform CharacterAttachPoint2;
      public float CharacterEffect2_DestroyTime = 10;
      [Space]

      public GameObject MainEffect;
      public Transform AttachPoint;
      public Transform OverrideAttachPointToTarget;
      public float Effect_DestroyTime = 10;
      [Space]

      public GameObject AdditionalEffect;
      public Transform AdditionalEffectAttachPoint;
      public float AdditionalEffect_DestroyTime = 10;
      
      
}
