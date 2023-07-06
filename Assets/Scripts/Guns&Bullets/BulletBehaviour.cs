using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 20;
    

    private Vector2 bounds;
    private float offset = 1f;
    //private float DestroyTime = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();


    }
    private void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        bounds+=new Vector2(offset,offset);
        rb.velocity = transform.right * speed;
    }
    // Start is called before the first frame update
    void Update()
    {
        if(Mathf.Abs(transform.position.x)>bounds.x || Mathf.Abs(transform.position.y) > bounds.y)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            collision.gameObject.GetComponent<ZombieBehaviour>().Die();
            Destroy(gameObject);
        }
    }

}
