using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;

    void Start()
    {
        if (!player)
        {
            Debug.LogWarning("Player transform is missing from Camera");
        }
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
}
