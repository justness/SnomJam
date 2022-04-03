using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemy : Enemy
{
    public Vector3 spawn;

    float speed = 5;
    float attackRecovery = 0;
    NavMeshAgent agent;
    bool resetting;

    Animator anim;

    public override void Attack()
    {
        agent.speed = 0;
        // Currently, damage is guaranteed. Should another collision be checked to see if the attack landed after getting in range?
        PlayerStats ps = player.GetComponent<PlayerStats>();
        ps.health -= attack;
        ps.CheckHealth();
        attackRecovery = 5;
    }
    public override void Follow()
    {
        agent.speed = speed;
        agent.destination = player.transform.position;
    }
    public override void Reset()
    {
        resetting = true;
        agent.speed = speed;
        agent.destination = spawn;
    }

    void Awake()
    {
        health = 10;
        attack = 10;
        spawn = transform.position;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (attackRecovery <= 0 && !resetting) {
            attackRecovery = 0;
            Follow();
        }
        else attackRecovery -= Time.deltaTime;

        if (agent.speed > 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player")) Attack();
    }
}
