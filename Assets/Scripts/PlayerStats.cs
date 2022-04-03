using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    Rigidbody rb;
    public int health = 100;
    int attack = 10;

    [SerializeField] Transform healthBar;

    EnemyManager manager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = FindObjectOfType<EnemyManager>();
    }

    void Update()
    {
        healthBar.transform.localRotation = Quaternion.Euler(Vector3.forward * (0.7f * (100 - health) + 20));
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
