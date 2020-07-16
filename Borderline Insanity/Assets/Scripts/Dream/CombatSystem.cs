using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Stats")]
    public float maxHealth;
    public float currentHealth;
    public float atkDamage;
    public float atkDistance;

    float maximumTimer = 0.5f, currentTimer;

    public bool attacking, timer_acttive;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        currentTimer = maximumTimer;
    }

    void Update()
    {

        if (Input.GetKeyDown("f"))
        {
            MeleeAttack();
        }

        if (GetComponent<DreamMovement>().blocking)
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
        }

        if (timer_acttive)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                attacking = false;
                timer_acttive = false;
            }
        }
    }

    void MeleeAttack()
    {
        if (transform.localScale.x > 0) // looking right
        {
            rb.velocity = new Vector2(atkDistance * 4, 2);//Vector2.right * (atkDistance * 4);
        }
        else // looking left
        {
            rb.velocity = new Vector2(-atkDistance * 4, 2);
        }
        attacking = true;
    }

    /*public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (attacking)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                //atkDamage = collision.gameObject.GetComponent<EnemyAI>().currentHealth;
                collision.gameObject.GetComponent<EnemyAI>().currentHealth -= atkDamage;
                Debug.Log("attacking");
                //Destroy(collision.gameObject);
                attacking = false;
            }
            else
            {
                timer_acttive = true;
            }
        }
    }
}
