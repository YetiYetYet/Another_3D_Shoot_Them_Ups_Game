using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    void Start () {
        var physicsMotion = GetComponentInChildren<RFX4_PhysicsMotion>(true);
        if (physicsMotion != null) physicsMotion.CollisionEnter += CollisionEnter;

        var raycastCollision = GetComponentInChildren<RFX4_RaycastCollision>(true);
        if(raycastCollision != null) raycastCollision.CollisionEnter += CollisionEnter;
    }

    private void CollisionEnter(object sender, RFX4_PhysicsMotion.RFX4_CollisionInfo e)
    {
        //Debug.Log(e.HitPoint); //a collision coordinates in world space
        //Debug.Log(e.HitGameObject.name); //a collided gameobject
        //Debug.Log(e.HitCollider.name);//a collided collider :)

        if (e.HitCollider.gameObject.CompareTag("Enemy"))
        {
            Enemy.Instance.TakeDamage(SkillsManager.Instance.actualSkill.damage);
        }
    }

}
