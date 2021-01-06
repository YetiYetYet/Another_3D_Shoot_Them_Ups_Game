using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public bool useHit;
    public float maxLifeTime = 8f;
    private GameObject _hit;
    private float _hitOffset = 0f;
    private bool _useFirePointRotation;
    private Vector3 _rotationOffset = new Vector3(0, 0, 0);
    

    private void OnEnable()
    {
        Enemy.NextState += OnNextEnemyState;
        Enemy.Die += OnDeath;
        Player.PlayerDeath += OnPlayerDeath;
    }

    
    private void OnDisable()
    {
        Enemy.NextState -= OnNextEnemyState;
        Enemy.Die -= OnDeath;
        Player.PlayerDeath -= OnPlayerDeath;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        var projectileMoverScript = gameObject.GetComponentInChildren<ProjectileMover>();
        projectileMoverScript.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        projectileMoverScript.gameObject.GetComponent<SphereCollider>().enabled = false;
        projectileMoverScript.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //Debug.Log(projectileMoverScript.gameObject.GetComponentsInChildren<ParticleSystem>().Length);
        foreach (var particleSystemChild in projectileMoverScript.gameObject.GetComponentsInChildren<ParticleSystem>())
        {
            /*var destroyScript = particleSystemChild.GetComponent<AutoDestroyPS>();
            if (destroyScript != null) destroyScript.enabled = false;*/
            particleSystemChild.tag = "Bullet";
            var main = particleSystemChild.main;
            main.loop = true;
        }
        projectileMoverScript.enabled = false;

        if (damage <= 0)
        {
            Debug.LogWarning("Damage of projectiles is <= 0, are you sure you want this ?");
        }

        if (useHit)
        {
            _hit = projectileMoverScript.hit;
            _hitOffset = projectileMoverScript.hitOffset;
            _useFirePointRotation = projectileMoverScript.UseFirePointRotation;
            _rotationOffset = projectileMoverScript.rotationOffset;
        }
        
        Destroy(gameObject, maxLifeTime);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
            
        Player.Instance.TakeDamage(damage);

        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point + contact.normal * _hitOffset;
        if (_hit != null)
        {
            var hitInstance = Instantiate(_hit, pos, rot);
            if (_useFirePointRotation) { hitInstance.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180f, 0); }
            else if (_rotationOffset != Vector3.zero) { hitInstance.transform.rotation = Quaternion.Euler(_rotationOffset); }
            else { hitInstance.transform.LookAt(contact.point + contact.normal); }

            var hitPs = hitInstance.GetComponent<ParticleSystem>();
            if (hitPs != null)
            {
                Destroy(hitInstance, hitPs.main.duration);
            }
            else
            {
                var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(hitInstance, hitPsParts.main.duration);
            }
        }
        Destroy(gameObject);
    }

    public void OnNextEnemyState(Enemy enemy)
    {
        DestroyBullet();
    }
    
    private void OnPlayerDeath(Player obj)
    {
        DestroyBullet();
    }
    
    private void OnDeath(Enemy enemy)
    {
        DestroyBullet();
    }


    public void DestroyBullet()
    {
        var hitInstance = Instantiate(_hit, transform.position, Quaternion.identity);
        var hitPs = hitInstance.GetComponent<ParticleSystem>();
        if (hitPs != null)
        {
            Destroy(hitInstance, hitPs.main.duration);
        }
        else
        {
            var hitPsParts = hitInstance.transform.GetChild(0).GetComponent<ParticleSystem>();
            Destroy(hitInstance, hitPsParts.main.duration);
        }
        if(gameObject)
            Destroy(gameObject);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            DestroyBullet();
        }
    }
}
