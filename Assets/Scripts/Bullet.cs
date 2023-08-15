using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        StartCoroutine(DestroyBullet());
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Debug.Log("Hello");
        }
    }

    internal void MoveBullet(Vector3 direction, float speed)
    {
        rigid.velocity = direction * speed;
        transform.up = direction;
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
