using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    Player player;
    LevelGenerator levelGenerator;

    public int pillarIndex = 0;

    void Start()
    {
        player = FindObjectOfType<Player>();
        levelGenerator = FindObjectOfType<LevelGenerator>();

        if (!player)
        {
            Debug.LogWarning("Player can't be found by Game Manager");
        }
        if (!levelGenerator)
        {
            Debug.LogWarning("Level Generator can't be found by Game Manager");
        }
    }


    public void MoveToNextPillar(bool hasDied, float endOfStickXPosition)
    {
        pillarIndex++;

        if (hasDied)
        {
            StartCoroutine(player.MovePlayer(endOfStickXPosition, true));
        }
        else
        {
            float destinationX = levelGenerator.pillars[pillarIndex].transform.position.x;
            StartCoroutine(player.MovePlayer(destinationX, false));

            levelGenerator.GenerateNewPillar();
        }
    }

}
