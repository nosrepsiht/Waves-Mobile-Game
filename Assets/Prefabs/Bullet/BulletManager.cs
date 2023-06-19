using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    public float lifeTime;
    public float damage;
    public bool playerIsOwner;

    void Start()
    {
        if (!playerIsOwner)
        {
            GetComponent<Renderer>().material = Materials.Singleton.red;
        }

        Invoke("Destroy", lifeTime);
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * 100f, ForceMode.Acceleration);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mob" && playerIsOwner)
        {
            other.GetComponent<MobManager>().TakeDamage(damage);
            Destroy(gameObject);
        }

        else if (other.tag == "Player" && !playerIsOwner && GameManager.Singleton.aliveMobsList.Count > 0)
        {
            other.GetComponent<PlayerManager>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    void Destroy()
    {
        Destroy(gameObject);
    }
}
