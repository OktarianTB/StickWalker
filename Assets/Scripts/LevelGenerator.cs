using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject pillar;
    public GameObject pillarParent;

    float minXSize = 0.6f;
    float maxXSize = 1.2f;

    float minSpacing = 2f;
    float maxSpacing = 7f;

    float distanceToLoad = 20f;
    float yPosition = -4f;

    void Start()
    {
        LoadNewSection(0);
    }


    public void LoadNewSection(float xStart)
    {
        float distance = distanceToLoad;
        float currentX = xStart;
        while(distance > 0)
        {
            float initialX = currentX;
            
            Vector3 pillarPosition = new Vector3(currentX, yPosition, 0);
            pillar = Instantiate(pillar, pillarPosition, Quaternion.identity);

            float pillarXSize = GetRandomPillarSize();
            pillar.transform.localScale = new Vector3(pillarXSize, 4, 1);
            pillar.transform.parent = pillarParent.transform;
            pillar.name = "Pillar";

            currentX += GetRandomSpacing();

            currentX += pillarXSize;
            distance -= currentX - initialX;
        }
    }

    float GetRandomPillarSize()
    {
        float randomXSize = Random.Range(minXSize, maxXSize);
        return randomXSize;
    }

    float GetRandomSpacing() // returns the space between 2 pillars
    {
        float randomSpacing = Random.Range(minSpacing, maxSpacing);
        return randomSpacing;
    }


}
