using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] GameObject cube;
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] PlayerController player;
    Vector3 currentPos;
    void Start()
    {
        currentPos = cube.transform.position;
        target = currentPos + Vector3.right * index;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 direction = player.transform.forward;
            Bullet bullet = Instantiate(bulletPrefab, player.transform.position + Vector3.up * 0.75f, Quaternion.identity);
            bullet.MoveBullet(direction, 10);

            player.PlayAnimation(PlayerController.State.ShootAutoshot_AR_Anim);
        }

        MoveCude();
        GetCollider();
    }

    Vector3 target;
    int index = 5;

    void MoveCude()
    {
        if (!cube) return;

        cube.transform.position = Vector3.MoveTowards(cube.transform.position, target, 0.01f);
        index *= -1;

        if (cube.transform.position == target)
        {
            target = currentPos + Vector3.right * index;
        }
    }

    void GetCollider()
    {
        if (!cube) return;

        Collider[] colliders = Physics.OverlapSphere(cube.transform.position, 1);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Bullet"))
            {
                Destroy(collider.gameObject);
                Destroy(cube);
                Debug.Log("Win");
            }
        }
    }
}
