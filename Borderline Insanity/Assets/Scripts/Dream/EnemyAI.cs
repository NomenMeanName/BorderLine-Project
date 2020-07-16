using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { RANGED, CLOSE, FRIEND_CLOSE, FRIEND_RANGED}

public class EnemyAI : MonoBehaviour
{
    public EnemyType type;
    GameObject player;
    Rigidbody2D rb;
    bool cooldown_activated;

    [Header("Don't Touch!")]
    public GameObject healthBar;
    public GameObject ammoSample;

    [Header("Enemy Stats")]
    public float enemySpeed;
    public float sightDist;
    public float atkDist;
    public float damage;
    public float maxHealth;
    public float max_cooldown;
    public float currentHealth;
    private float cooldown;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        cooldown = max_cooldown;
        if (type == EnemyType.RANGED || type == EnemyType.FRIEND_RANGED)
            atkDist = sightDist;
        else if (type == EnemyType.FRIEND_CLOSE || type == EnemyType.FRIEND_RANGED)
            gameObject.tag = "Friend";
    }

    void Update()
    {
        var dist = Vector2.Distance(player.transform.position ,transform.position);
        var friendDist = Vector2.Distance(FindClosestFriend().transform.position, transform.position);
        var enemyDist = Vector2.Distance(FindClosestEnemy().transform.position, transform.position);
        //Debug.Log(dist);

        if (currentHealth <= 0)
        {
            //Destroy(gameObject);
        }

        if (dist < sightDist)
        {
            healthBar.SetActive(true);
            healthBar.transform.localScale = new Vector3(currentHealth / maxHealth, healthBar.transform.localScale.y);
            switch (type)
            {
                #region Enemy - Close
                case EnemyType.CLOSE:

                    if (friendDist <= sightDist) // If friend is close
                    {
                        var enemyPos = FindClosestFriend().transform.position;

                        if (enemyPos.x > transform.position.x) // On right
                        {
                            rb.velocity = new Vector2(enemySpeed * Time.deltaTime, rb.velocity.y);
                            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
                        }
                        else if (enemyPos.x < transform.position.x) // On left
                        {
                            rb.velocity = new Vector2(-enemySpeed * Time.deltaTime, rb.velocity.y);
                            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y);
                        }
                        if (friendDist <= atkDist)
                        {
                            rb.velocity = Vector2.zero;
                            if (cooldown_activated == false)
                                MeleeAttack();
                        }
                    }
                    else
                    {
                        if (player.transform.position.x > transform.position.x) // On right
                        {
                            rb.velocity = new Vector2(enemySpeed * Time.deltaTime, rb.velocity.y);
                            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
                        }
                        if (player.transform.position.x < transform.position.x) // On left
                        {
                            rb.velocity = new Vector2(-enemySpeed * Time.deltaTime, rb.velocity.y);
                            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y);
                        }
                        if (dist <= atkDist && player.transform.position.y == transform.position.y)
                        {
                            rb.velocity = Vector2.zero;
                            if (cooldown_activated == false)
                                MeleeAttack();
                        }
                    }                    
                    break;
                #endregion

                #region Enemy - Ranged
                case EnemyType.RANGED:
                    if (player.transform.position.x > transform.position.x) // On right
                    {
                        transform.localScale = new Vector3(-2, 4);
                    }
                    if (player.transform.position.x < transform.position.x) // On left
                    {
                        transform.localScale = new Vector3(2, 4);
                    }

                    if (dist <= atkDist && cooldown_activated == false)
                    {
                        RangedAttack();
                    }
                    break;
                #endregion

                #region Friend - Close
                case EnemyType.FRIEND_CLOSE:
                    if (player.transform.position.x > transform.position.x) // On right
                    {
                        rb.velocity = new Vector2(enemySpeed * Time.deltaTime, rb.velocity.y);
                        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
                    }
                    if (player.transform.position.x < transform.position.x) // On left
                    {
                        rb.velocity = new Vector2(-enemySpeed * Time.deltaTime, rb.velocity.y);
                        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y);
                    }
                    if (dist <= atkDist)
                    {
                        rb.velocity = Vector2.zero;
                    }
                    if (Vector2.Distance(FindClosestEnemy().transform.position, transform.position) <= sightDist)
                    {
                        var enemyPos = FindClosestEnemy().transform.position;

                        if (enemyPos.x > transform.position.x) // On right
                        {
                            rb.velocity = new Vector2(enemySpeed * Time.deltaTime, rb.velocity.y);
                            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
                        }
                        else if(enemyPos.x < transform.position.x) // On left
                        {
                            rb.velocity = new Vector2(-enemySpeed * Time.deltaTime, rb.velocity.y);
                            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y);
                        }
                        if (enemyDist <= atkDist)
                        {
                            rb.velocity = Vector2.zero;
                            if (cooldown_activated == false)
                                MeleeAttack();
                        }
                    }
                    break;
                #endregion

                #region Friend - Ranged
                case EnemyType.FRIEND_RANGED:
                    if (enemyDist <= sightDist)
                    {
                        if (FindClosestEnemy().transform.position.x > transform.position.x) // On right
                        {
                            transform.localScale = new Vector3(-2, 4);
                        }
                        if (FindClosestEnemy().transform.position.x < transform.position.x) // On left
                        {
                            transform.localScale = new Vector3(2, 4);
                        }
                        if (enemyDist <= atkDist && cooldown_activated == false)
                        {
                          RangedAttack();
                        }
                    }
                    break;
                    #endregion
            }
        }
        else
        {
            healthBar.SetActive(false);
        }

        // Attack Cooldown
        if (cooldown_activated)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                cooldown_activated = false;
                cooldown = max_cooldown;
            }
        }
        
    }

    void MeleeAttack()
    {       
        // Play an attack animation
        if (type == EnemyType.CLOSE)
        {
            var friendDist = Vector2.Distance(FindClosestFriend().transform.position, transform.position);
            var dist = Vector2.Distance(player.transform.position, transform.position);

            if (friendDist < dist) // if the friend is closer
            {
                var enemyAI = FindClosestFriend().GetComponent<EnemyAI>();
                var enemy_rb = FindClosestFriend().GetComponent<Rigidbody2D>();

                if (FindClosestFriend().transform.position.x > transform.position.x) // On right
                {
                    enemy_rb.AddForce(new Vector2(damage * 100, damage * 10));
                }
                if (FindClosestFriend().transform.position.x < transform.position.x) // On left
                {
                    enemy_rb.AddForce(new Vector2(-damage * 100, damage * 10));
                }
                enemyAI.currentHealth -= damage;
            }
            else if (dist < friendDist) // If player is closer
            {
                var player_combat = player.GetComponent<CombatSystem>();
                var player_rb = player.GetComponent<Rigidbody2D>();

                if (player.transform.position.x > transform.position.x) // On right
                {
                    player_rb.AddForce(new Vector2(damage * 100, damage * 10));
                    Debug.Log("on right");
                }
                if (player.transform.position.x < transform.position.x) // On left
                {
                    player_rb.AddForce(new Vector2(-damage * 100, damage * 10));
                    Debug.Log("on left");
                }

                if (player.GetComponent<DreamMovement>().blocking == false)
                {
                    player_combat.currentHealth -= damage;
                }
            }

        }
        else if (type == EnemyType.FRIEND_CLOSE)
        {
            var enemyAI = FindClosestEnemy().GetComponent<EnemyAI>();
            var enemy_rb = FindClosestEnemy().GetComponent<Rigidbody2D>();

            if (FindClosestEnemy().transform.position.x > transform.position.x) // On right
            {
                enemy_rb.AddForce(new Vector2(damage * 100, damage * 10));
            }
            if (FindClosestEnemy().transform.position.x < transform.position.x) // On left
            {
                enemy_rb.AddForce(new Vector2(-damage * 100, damage * 10));
            }

            enemyAI.currentHealth -= damage;
        }

        cooldown_activated = true;
    }

    void RangedAttack()
    {
        var ammo = Instantiate(ammoSample, transform.position - new Vector3 (transform.localScale.x / 4,0), Quaternion.identity);
        ammo.GetComponent<AmmoScript>().enabled = true;

        if (transform.localScale.x > 0)
        ammo.GetComponent<AmmoScript>().speed = -ammo.GetComponent<AmmoScript>().speed;
        cooldown_activated = true;
        Debug.Log("shot");
    }

    public GameObject FindClosestEnemy()
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
            if (curDistance <= distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public GameObject FindClosestFriend()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Friend");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance <= distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
}
