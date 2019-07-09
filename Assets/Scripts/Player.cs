using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float timeToMoveOneUnit = 5f;
    LevelGenerator levelGenerator;
    StickManager stickManager; 

    private void Start()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
        stickManager = FindObjectOfType<StickManager>();

        if (levelGenerator)
        {
            float startPositionX = levelGenerator.pillars[0].transform.position.x;
            transform.position = new Vector3(startPositionX, transform.position.y, 0f);
        }

        if (!stickManager)
        {
            Debug.LogWarning("Stick Manager is missing from Player");
        }
    }

    public IEnumerator MovePlayer(float xDestination, bool hasLost)
    {
        float elapsedTime = 0f;
        float timeToMove = (xDestination - transform.position.x) / timeToMoveOneUnit;

        Vector3 startPosition = transform.position;
        Vector3 destination = new Vector3(xDestination, transform.position.y, 0f);

        while (transform.position.x < xDestination)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f);
            t = t * t * t * (t * (t * 6 - 15) + 10);

            transform.position = Vector3.Lerp(startPosition, destination, t);

            yield return null;
        }

        if (!hasLost)
        {
            stickManager.ResetStick();
        }
        else
        {
            StartCoroutine(PlayerFall());
        }

    }

    private IEnumerator PlayerFall()
    {
        float elapsedTime = 0f;
        float timeToMove = 1f;

        Vector3 startPosition = transform.position;
        Vector3 destination = new Vector3(transform.position.x, -10f, 0f);

        while (transform.position.y > destination.y)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f);
            t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f);

            transform.position = Vector3.Lerp(startPosition, destination, t);

            yield return null;
        }
    }
}
