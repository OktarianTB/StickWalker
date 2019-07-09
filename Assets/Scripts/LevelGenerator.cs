using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject pillarPrefab;
    public GameObject pillarParent;

    float minXSize = 0.5f;
    float maxXSize = 1.3f;

    float minSpacing = 2f;
    float maxSpacing = 5f;
    
    float yPosition = -4.5f;
    float currentX = 0;

    public int pillarNumber = 0;
    public List<GameObject> pillars;

    void Start()
    {
        if(!pillarPrefab || !pillarParent)
        {
            Debug.LogError("Pillar is missing from level generator");
        }

        pillars = new List<GameObject>();

        for (int i = 0; i < 10; i++)
        {
            GenerateNewPillar();
        }
    }

    public void GenerateNewPillar()
    {
        Vector3 pillarPosition = new Vector3(currentX, yPosition, 0);
        GameObject pillar = Instantiate(pillarPrefab, pillarPosition, Quaternion.identity);

        float pillarXSize = GetRandomPillarSize();
        pillar.transform.localScale = new Vector3(pillarXSize, 4, 1);
        pillar.transform.parent = pillarParent.transform;
        pillar.name = "Pillar " + pillarNumber.ToString();
        pillars.Add(pillar);

        currentX += GetRandomSpacing();
        currentX += pillarXSize;
        pillarNumber++;
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
