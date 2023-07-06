using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCameraBahaviour : MonoBehaviour
{
    private void Update()
    {
        Transform player= GameObject.Find("Player").transform;
        transform.position= new Vector3(player.position.x, player.position.y, -1);
    }
}
