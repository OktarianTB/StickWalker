using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    float timeToMoveOneUnit = 5f;

    public IEnumerator MovePlayer(float xDestination)
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

    }

}
