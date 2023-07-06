using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieBehaviour : MonoBehaviour
{
    [SerializeField]
    private Sprite[] deathSprites;
    private GameObject player;
    private float speed = 1f;
    [SerializeField]
    private GameObject marker;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Move") &&animator.enabled)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(Vector3.forward, -dir) * Quaternion.Euler(0, 0, -90f);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, 360f * Time.deltaTime);
            transform.position += dir * speed * Time.deltaTime;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().OpenLosePanel();
        }
        if (collision.gameObject.CompareTag("Zombie"))
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Zombie_Move"))
            {
                Vector2 spawnpos = player.transform.position;
                Vector2 offset = new Vector2(UnityEngine.Random.Range(2f, 5f), UnityEngine.Random.Range(2f, 5f));
                offset.x = UnityEngine.Random.Range(0, 2) == 1 ? offset.x : -offset.x;
                offset.y = UnityEngine.Random.Range(0, 2) == 1 ? offset.y : -offset.y;
                spawnpos += offset;
                this.transform.position = spawnpos;
            }
        }
    }
    public void Die()
    {
        Destroy(marker);
        GetComponent<SpriteRenderer>().sortingOrder = 0;
        GetComponent<Collider2D>().enabled = false;
        animator.enabled = false;
        GetComponent<SpriteRenderer>().sprite = deathSprites[UnityEngine.Random.Range(0,deathSprites.Length)];
        EventHandler.Instance.OnZombieDeath.Invoke();
        string soundname = "ZombieDeath" + UnityEngine.Random.Range(1, 5);
        FindObjectOfType<AudioManager>().Play(soundname);
        Destroy(gameObject,5f);
    }
}
