using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public LayerMask collisionMask;
    public AudioClip impactClip;
    GameManager gameManager;

    float volume = 0.7f;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Debug.LogWarning("Stick Manager can't be found by Stick script");
        }
        if (!impactClip)
        {
            Debug.LogWarning("Impact clip is missing from Stick Manager");
        }
    }
    
    public void CheckForCollision() // Checks whether the end of the stick is on the pillar
    {
        RaycastHit2D hit = Physics2D.Raycast(GetRayOrigin(), new Vector2(0, -1f), 0.5f, collisionMask);
        Debug.DrawRay(GetRayOrigin(), -Vector2.up, Color.blue);

        float endOfStickXPosition = transform.localScale.y + transform.position.x;

        if (hit)
        {
            AudioSource.PlayClipAtPoint(impactClip, transform.position, volume);
            gameManager.MoveToNextPillar(false, endOfStickXPosition);
        }
        else
        {
            gameManager.MoveToNextPillar(true, endOfStickXPosition);
            gameManager.gameIsOver = true;
        }
    }

    Vector2 GetRayOrigin() // origin of the ray: top-right corner but origin of the stick: bottom-center with the the stick rotated 90 degrees
    {
        float height = transform.localScale.y;
        float width = transform.localScale.x;

        float x = transform.position.x + height;
        float y = transform.position.y - (width / 2);

        return new Vector2(x, y);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}
