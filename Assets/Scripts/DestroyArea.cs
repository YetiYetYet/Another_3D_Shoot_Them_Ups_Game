using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyArea : MonoBehaviour
{
    public void Start()
    {
        if (gameObject.transform.GetComponent<Collider>() == null)
        {
            Debug.LogError("Require collider attached to script");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
