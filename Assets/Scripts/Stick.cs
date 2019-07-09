using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public LayerMask collisionMask;
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Debug.LogWarning("Stick Manager can't be found by Stick script");
        }
    }
    
    public void CheckForCollision()
    {
        RaycastHit2D hit = Physics2D.Raycast(GetRayOrigin(), new Vector2(0, -1f), 0.5f, collisionMask);
        Debug.DrawRay(GetRayOrigin(), -Vector2.up, Color.blue);

        float endOfStickXPosition = transform.localScale.y + transform.position.x;

        if (hit)
        {
            gameManager.MoveToNextPillar(false, endOfStickXPosition);
        }
        else
        {
            gameManager.MoveToNextPillar(true, endOfStickXPosition);
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
