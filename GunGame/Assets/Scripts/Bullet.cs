using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] float speed;
    [SerializeField] float timeIsActivity;
    [SerializeField] int damage;

    enum Tags
    {
        Enemy,
        Item,
    }
    

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidBody.velocity = -transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;

        try
        {
            switch ((Tags)Enum.Parse(typeof(Tags), collision.gameObject.tag))
            {
                case Tags.Enemy:
                    obj.GetComponent<EnemyBehavior>().Damage(damage);
                    break;
                case Tags.Item:
                    obj.GetComponent<ItemBooster>().ToDestroy();
                    break;
            }
        }
        catch { }

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(TimerActive());
    }

    IEnumerator TimerActive()
    {
        yield return new WaitForSeconds(timeIsActivity);
        gameObject.SetActive(false);
    }

    public void SetDamage(int d)
    {
        damage = d;
    }
}
