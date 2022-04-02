using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedHUD : MonoBehaviour
{
    public GameObject player;
    float maxFast = 15f;
    
    public ParticleSystem speedlines;

    void Start(){
        var em = speedlines.emission;
        em.rateOverTime = 0f;
    }

    void Update()
    {
        var em = speedlines.emission;
        if (player.GetComponent<PlayerMovement>().dashing) {
            em.rateOverTime = 100f * (player.GetComponent<Rigidbody>().velocity.magnitude/maxFast);
        }
        else em.rateOverTime = 0f;
    }
}
