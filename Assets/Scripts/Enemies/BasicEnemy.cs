using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Enemy
{
    public GameObject spawn;
    public GameObject player;

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
        GetComponent<UnityEngine.AI.NavMeshAgent>().destination = startPos;
    }

    void Start()
    {
        health = 10;
        attack = 10;
        startPos = spawn.transform.position;
    }

    void Update()
    {
        Follow();
    }
}
