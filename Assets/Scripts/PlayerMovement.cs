using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private float horizontal, vertical,speed=150;
    private Vector2 bounds;
    private Vector2 mousePos;
    private float objWidth, objHeight;
    private bool soundisPlaying = false;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

    }
    void Start()
    {
        bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objHeight = spriteRenderer.bounds.size.y / 2;
        objWidth = spriteRenderer.bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (horizontal == 0 && vertical == 0)
        {
            animator.SetBool("isMoving", false);
            if(soundisPlaying)
            {
                FindObjectOfType<AudioManager>().Stop("Walk");
                soundisPlaying = false;
            }
        }
        else
        {
            animator.SetBool("isMoving", true);
            if(!soundisPlaying)
            {
                FindObjectOfType<AudioManager>().Play("Walk");
                soundisPlaying = true;

            }
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal, vertical) * speed*Time.fixedDeltaTime;
        Vector2 lookDir = mousePos - rb.position;
        rb.rotation = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
    }
    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, bounds.x * -1 + objWidth, bounds.x - objWidth);
        pos.y = Mathf.Clamp(pos.y, bounds.y * -1 + objHeight, bounds.y - objHeight);
        transform.position = pos;
    }
}
