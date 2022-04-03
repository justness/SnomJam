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

    EnemyManager manager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = FindObjectOfType<EnemyManager>();
    }

    void Update()
    {
        // TODO: Update this to work for a circular/non-linear healthbar.
        //healthbar.GetComponent<RectTransform>().sizeDelta =  new Vector2 (18*health, healthbar.GetComponent<RectTransform>().sizeDelta.y);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") && gameObject.GetComponent<PlayerMovement>().dashing) {
            Debug.Log("hit enemy");
            
            if (collision.gameObject.GetComponent<Enemy>().health <= attack * gameObject.GetComponent<PlayerMovement>().dashCount)
            {
                manager.RemoveSpawn(collision.gameObject.GetComponent<Enemy>());
                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.tag.Equals("Breakable")) {
            // Break object if attack value * dashCount > breakable's integrity value
        }
    }
}
