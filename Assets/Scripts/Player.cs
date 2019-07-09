using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    LevelGenerator levelGenerator;
    StickManager stickManager;
    GameManager gameManager;
    Animator animator;

    public AudioClip successClip;
    public AudioClip uhClip;
    float timeToMoveOneUnit = 5f;
    float volumeSuccess = 0.7f;
    float volumeUh = 1.2f;

    private void Start()
    {
        levelGenerator = FindObjectOfType<LevelGenerator>();
        stickManager = FindObjectOfType<StickManager>();
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();

        if (levelGenerator)
        {
            float startPositionX = levelGenerator.pillars[0].transform.position.x;
            transform.position = new Vector3(startPositionX, transform.position.y, 0f);
        }
        else
        {
            Debug.LogWarning("Level Generator can't be found by Player");
        }

        if (!stickManager)
        {
            Debug.LogWarning("Stick Manager can't be found by Player");
        }

        if (!animator)
        {
            Debug.LogWarning("Animator component is missing from Player");
        }
        if (!gameManager)
        {
            Debug.LogWarning("Game Manager can't be found by Player");
        }
        if (!successClip)
        {
            Debug.LogWarning("Success clip is missing from Player");
        }
        if (!uhClip)
        {
            Debug.LogWarning("Uh clip is missing from Player");
        }
    }

    public IEnumerator MovePlayer(float xDestination, bool hasLost)
    {
        animator.SetBool("isMoving", true);

        float elapsedTime = 0f;
        float timeToMove = (xDestination - transform.position.x) / timeToMoveOneUnit;

        Vector3 startPosition = transform.position;
        Vector3 destination = new Vector3(xDestination, transform.position.y, 0f);

        while (transform.position.x < xDestination)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f);
            t = t * t * t * (t * (t * 6 - 15) + 10); // smoother movement

            transform.position = Vector3.Lerp(startPosition, destination, t);

            yield return null;
        }

        animator.SetBool("isMoving", false);

        if (!hasLost)
        {
            AudioSource.PlayClipAtPoint(successClip, transform.position, volumeSuccess);
            gameManager.UpdateScore();
            stickManager.ResetStick();
        }
        else
        {
            StartCoroutine(PlayerFall());
        }

    }

    private IEnumerator PlayerFall()
    {
        AudioSource.PlayClipAtPoint(uhClip, transform.position, volumeUh);

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
        gameManager.ManageGameOver();
    }

}
