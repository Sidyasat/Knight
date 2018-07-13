using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    public GameObject player;
    public float dist;
    NavMeshAgent nav;
    public float radiusDistance = 6f;
    Animator animator;
    public int HP = 100;
	// Use this for initialization
	void Start () {
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
	}

    public void IdleEnemy()
    {
        if (dist > radiusDistance)
        {
            nav.enabled = false;
            animator.SetTrigger("Idle");
        }
    }
	
    public void RunEnemy()
    {
        if (dist < radiusDistance && dist > 1.5f)
        {
            nav.enabled = true;
            nav.SetDestination(player.transform.position);
            animator.SetTrigger("Run");
        }
    }

    public void AttackEnemy()
    {
        if(dist < 1.5f)  
            {
            nav.enabled = false;
            animator.SetTrigger("Attack");
        }
    }

	// Update is called once per frame
	void Update () {
        if (HP <= 0)
        {
            animator.SetTrigger("Dead");
        }
        else
        {
            dist = Vector3.Distance(player.transform.position, transform.position);

            IdleEnemy();

            RunEnemy();

            AttackEnemy();
        }
	}
    void OnTriggerEnter(Collider collider)
    {
        if(collider.tag == "SwordPlayer")
        {
            HP -= 50;
        }
    }
}
