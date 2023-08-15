using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;

    public enum State
    {
        ShootAutoshot_AR_Anim,
        RunFWD_AR_Anim,
        Idle_AR_Anim
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (agent.velocity.magnitude != 0)
        {
            PlayAnimation(State.RunFWD_AR_Anim);
        }
        else
        {
            PlayAnimation(State.Idle_AR_Anim);
        }
        // animator.SetFloat("Speed", agent.velocity.magnitude);

        bool shooting = animator.GetCurrentAnimatorStateInfo(0).IsName(State.ShootAutoshot_AR_Anim.ToString());

        if (shooting)
        {
            agent.SetDestination(transform.position);
        }


        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    internal void PlayAnimation(State state)
    {
        animator.CrossFade(state.ToString(), 0);
    }
}
