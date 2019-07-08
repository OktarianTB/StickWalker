﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManager : MonoBehaviour
{

    public GameObject stickPrefab;
    GameManager gameGenerator;
    LevelGenerator levelGenerator;
    GameObject stick;

    bool drawingStick = false;
    float timePeriod = 0.03f;
    float time;
    float distancePerPeriod = 0.1f;

    float timeToRotate = 0.8f;

    bool hasRotated = false;
    bool canSpawnStick = true;

    void Start()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
        gameGenerator = FindObjectOfType<GameManager>();
        time = timePeriod;

        if (!stickPrefab)
        {
            Debug.LogError("Stick is missing from Stick Manager");
        }
        if (!gameGenerator)
        {
            Debug.LogWarning("Level Generator hasn't been found by Stick Manager");
        }
        if (!levelGenerator)
        {
            Debug.LogWarning("Level Generator can't be found by Game Manager");
        }
    }
    
    void Update()
    {
        ManageStickSpawn();
    }

    void ManageStickSpawn()
    {
        if (Input.GetMouseButtonDown(0) && canSpawnStick)
        {
            SpawnStick();
        }
        if (Input.GetMouseButtonUp(0) && !hasRotated)
        {
            drawingStick = false;
            StartCoroutine(RotateStick(timeToRotate));
        }

        if (drawingStick)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                DrawStick();
                time = timePeriod;
            }
        }
    }

    void DrawStick()
    {
        float xScale = stick.transform.localScale.x;
        float yScale = stick.transform.localScale.y + distancePerPeriod;
        stick.transform.localScale = new Vector3(xScale, yScale, 1);
    }

    IEnumerator RotateStick(float timeToMove)
    {
        float currentRotation = 0;
        float elapsedTime = 0f;
        while(currentRotation < 90 && elapsedTime < timeToMove)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f);
            t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f);

            float newRotation = Mathf.Lerp(0, 90, t);
            float degreesToRotate = newRotation - currentRotation;
            stick.transform.Rotate(0f, 0f, -degreesToRotate, Space.Self);

            currentRotation = newRotation;

            yield return null;
        }

        hasRotated = true;
        stick.GetComponent<Stick>()?.CheckForCollision();
    }

    void SpawnStick()
    {
        stick = Instantiate(stickPrefab, GetStickSpawnPosition(gameGenerator.pillarIndex), Quaternion.identity);
        drawingStick = true;
        canSpawnStick = false;
    }

    Vector3 GetStickSpawnPosition(int index)
    {
        GameObject pillar = levelGenerator.pillars[index];

        if (!pillar)
        {
            Debug.LogWarning("Pillar " + index.ToString() + " can't be found");
            return Vector3.zero;
        }
    
        float pillarWidth = pillar.transform.localScale.x;
        float pillarHeight = pillar.transform.localScale.y;

        float xPos = pillar.transform.position.x;
        float yPos = pillar.transform.position.y;

        float xStick = xPos + (pillarWidth / 2);
        float yStick = yPos + (pillarHeight / 2);

        return new Vector3(xStick, yStick, 0);
    }

}
