using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    float distance = 4f;

    void Start()
    {
        if (!player)
        {
            Debug.LogWarning("Player transform is missing from Camera");
        }
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x + distance, transform.position.y, transform.position.z);
    }
}
