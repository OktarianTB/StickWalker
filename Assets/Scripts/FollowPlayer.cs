using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
  
    public Transform player;
    float offset = 4f;

    public Material[] skyboxes;

    void Start()
    {
        SetRandomSkybox();

        if (!player)
        {
            Debug.LogWarning("Player transform is missing from Camera");
        }
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x + offset, transform.position.y, transform.position.z);
    }

    void SetRandomSkybox()
    {
        int randomIndex = Random.Range(0, skyboxes.Length);
        Material randomSkybox = skyboxes[randomIndex];

        RenderSettings.skybox = randomSkybox;
    }
}
