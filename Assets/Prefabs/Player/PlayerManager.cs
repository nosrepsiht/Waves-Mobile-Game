using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    PlayerReferences p;

    public bool canAttack;

    void FixedUpdate()
    {
        if (SceneObjects.Singleton.redforegroundImage.color.a > 0f)
        {
            RedScreenFading();
        }
    }

    void Update()
    {
        SceneObjects.Singleton.hpSlider.value = p.stats.hp / p.stats.hpMax;
        SceneObjects.Singleton.hp.text = p.stats.hp.ToString("0");
        SceneObjects.Singleton.ap.text = p.stats.ap.ToString("0");
        SceneObjects.Singleton.dp.text = p.stats.dp.ToString("0");
        SceneObjects.Singleton.coins.text = p.stats.coins.ToString("0");
        SceneObjects.Singleton.completedWaves.text = GameManager.Singleton.completedWaves + " / 3";
        SceneObjects.Singleton.aliveMobs.text = GameManager.Singleton.aliveMobsList.Count.ToString();

        if (SceneObjects.Singleton.joystickRotation.InputDirection.magnitude > 0)
        {
            Attack();
        }
    }

    // Player takes damage
    public void TakeDamage(float _damage)
    {
        AudioManager.Singleton.Play("PlayerHit");

        if (_damage > p.stats.dp)
        {
            p.stats.hp += p.stats.dp - _damage;
            SceneObjects.Singleton.redforegroundImage.color = new Color(1f, 0f, 0f, 0.5f);

            if (p.stats.hp <= 0f)
            {
                AudioManager.Singleton.Play("PlayerDeath");
                SceneObjects.Singleton.redforegroundImage.color = new Color(1f, 0f, 0f, 0f);
                SceneObjects.Singleton.mainCamera.transform.parent = null;
                GameManager.Singleton.PlayerIsDead();
                Destroy(gameObject);
            }
        }
    }

    public void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            Invoke("RestoreAttack", 0.2f);

            AudioManager.Singleton.Play("PlayerShot");
            GameObject _fireball = Instantiate(Prefabs.Singleton.bullet, transform.position + new Vector3(0f, 0.5f, 0f) + p.rotationTransform.forward, p.rotationTransform.rotation);
            _fireball.GetComponent<BulletManager>().damage = p.stats.ap;
            _fireball.GetComponent<BulletManager>().playerIsOwner = true;
        }
    }

    void RestoreAttack()
    {
        canAttack = true;
    }

    void RedScreenFading()
    {
        SceneObjects.Singleton.redforegroundImage.color = new Color(1f, 0f, 0f, SceneObjects.Singleton.redforegroundImage.color.a - 0.01f);
    }
}
