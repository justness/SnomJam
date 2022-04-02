using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public Vector3 spawn;
    public GameObject player;

    float attackRange = 3;

    public Material lit;
    public Material red;

    public override void Attack()
    {
        // Play animation, Apply any effects
        return;
    }
    public override void Follow()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().destination = player.transform.position;
    }
    public override void Reset()
    {
        GetComponent<UnityEngine.AI.NavMeshAgent>().destination = spawn;
    }

    void Awake()
    {
        health = 10;
        attack = 10;
        spawn = transform.position;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    void Update()
    {
        Follow();

        if (Vector3.Distance(player.transform.position, transform.position) < attackRange){
            GetComponent<MeshRenderer>().material = red;
        }
        else GetComponent<MeshRenderer>().material = lit;
    }
}
