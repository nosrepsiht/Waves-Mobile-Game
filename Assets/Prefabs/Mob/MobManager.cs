using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour
{
    [SerializeField]
    MobReferences m;

    public bool canAttack;
    public float distance;

    void Start()
    {
        m.canvas.mobName.text = m.stats.mobName;
    }

    void Update()
    {
        m.agent.SetDestination(GameManager.Singleton.player.transform.position);
        m.rotationTransform.transform.LookAt(new Vector3(GameManager.Singleton.player.transform.position.x, transform.position.y, GameManager.Singleton.player.transform.position.z));

        distance = Vector3.Distance(transform.position, GameManager.Singleton.player.transform.position);

        if (distance <= 10f)
        {
            if (canAttack)
            {
                canAttack = false;
                Invoke("RestoreAttack", 1f);

                AudioManager.Singleton.Play("MobShot");
                GameObject _fireball = Instantiate(Prefabs.Singleton.bullet, transform.position + new Vector3(0f, 0.5f, 0f) + m.rotationTransform.forward, m.rotationTransform.rotation);
                _fireball.GetComponent<BulletManager>().damage = m.stats.ap;
            }
        }

        m.canvas.hp.text = m.stats.hp.ToString("0");
        m.canvas.ap.text = m.stats.ap.ToString("0");
        m.canvas.dp.text = m.stats.dp.ToString("0");

        m.canvas.hpSlider.value = m.stats.hp / m.stats.hpMax;
    }

    void RestoreAttack()
    {
        canAttack = true;
    }
    
    // Mobs takes damage
    public void TakeDamage(float _damage)
    {
        AudioManager.Singleton.Play("MobHit");

        if (_damage > m.stats.dp)
        {
            m.stats.hp += m.stats.dp - _damage;

            if (m.stats.hp <= 0f)
            {
                AudioManager.Singleton.Play("MobDeath");
                GameManager.Singleton.aliveMobsList.Remove(gameObject);
                GameManager.Singleton.MobIsKilled();
                Destroy(gameObject);
            }
        }
    }
}
