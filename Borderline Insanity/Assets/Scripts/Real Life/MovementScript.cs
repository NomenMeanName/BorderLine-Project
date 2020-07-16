using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    Rigidbody2D rb;
    public float speed;

    private float size_x, size_y;

    public GameObject idle_anim, walking_anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        size_x = transform.localScale.x;
        size_y = transform.localScale.y;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey("d"))
        {
            rb.velocity = new Vector2(speed, rb.velocity.y); //Moves the player right
            transform.localScale = new Vector3(size_x, transform.localScale.y); // Makes the player look right
            idle_anim.SetActive(false);
            walking_anim.SetActive(true);
        }
        else if (Input.GetKey("a"))
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y); //Moves the player left
            transform.localScale = new Vector3(-size_x, transform.localScale.y); // Makes the player look left
            idle_anim.SetActive(false);
            walking_anim.SetActive(true);
        }
        else
        {
            idle_anim.SetActive(true);
            walking_anim.SetActive(false);
        }
    }
}

