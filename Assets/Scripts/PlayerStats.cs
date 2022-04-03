using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    Rigidbody rb;
    public int health = 100;
    int attack = 10;

    public Image healthbar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // TODO: Update this to work for a circular/non-linear healthbar.
        //healthbar.GetComponent<RectTransform>().sizeDelta =  new Vector2 (18*health, healthbar.GetComponent<RectTransform>().sizeDelta.y);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" && gameObject.GetComponent<PlayerMovement>().dashing) {
            Debug.Log("hit enemy");
            if (collision.gameObject.GetComponent<Enemy>().health <= attack * gameObject.GetComponent<PlayerMovement>().dashCount) Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Breakable") {
            // Break object if attack value * dashCount > breakable's integrity value
        }
    }
}
