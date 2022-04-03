using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] int maxHealth = 100;
    public int health;
    int attack = 10;

    [SerializeField] Transform healthBar;

    EnemyManager manager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = FindObjectOfType<EnemyManager>();
        health = maxHealth;
    }

    void Update()
    {
        healthBar.transform.localRotation = Quaternion.Euler(Vector3.forward * (0.7f * (100 - health) + 20));
        if (health <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy") && gameObject.GetComponent<PlayerMovement>().dashing) {
            int enemyHealth = collision.gameObject.GetComponent<Enemy>().health;
            if (enemyHealth <= attack * gameObject.GetComponent<PlayerMovement>().dashCount)
            {
                manager.RemoveSpawn(collision.gameObject.GetComponent<Enemy>());

                if (health + enemyHealth < maxHealth) health += enemyHealth;
                else health = maxHealth;

                Destroy(collision.gameObject);
            }
        }

        if (collision.gameObject.tag.Equals("Breakable")) {
            // Break object if attack value * dashCount > breakable's integrity value
        }
    }
}
