using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBahaviour : MonoBehaviour
{
    [SerializeField]
    private Sprite playerSprite;
    [SerializeField]
    private Transform[] firePoints;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float delay;

    private GameObject player;
    private bool canFire = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        GameObject.Find("Body").GetComponent<SpriteRenderer>().sprite = playerSprite;
    }

    // Update is called once per frame
    void Update()
    {
        

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && Time.timeScale==1f)
        {
            if (canFire)
            {
                StartCoroutine(Fire());
            }
        }


    }
    private IEnumerator Fire()
    {
        if (!canFire)
            yield break;
        canFire = false;
        foreach (Transform point in firePoints)
        {
            Instantiate(bulletPrefab, point.position, point.rotation);
            //Instantiate(bulletPrefab, point.position, player.transform.rotation);
        }
        FindObjectOfType<AudioManager>().Play("Shoot");
        yield return new WaitForSeconds(delay);
        canFire = true;
    }
}
