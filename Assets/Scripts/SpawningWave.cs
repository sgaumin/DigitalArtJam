using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningWave : MonoBehaviour
{
    [SerializeField] private GuideMovement guidePrefab;
    [SerializeField] private Visitor visitorPrefab;

    [SerializeField] private float spawningTime;
    [SerializeField] private Transform[] spawningVisitors;
    [SerializeField] private Transform spawningGuide;

    [SerializeField] private int numberSpawnMin;
    [SerializeField] private int numberSpawnMax;

    void Start()
    {
        StartCoroutine(SpawningWaves());
    }

    IEnumerator SpawningWaves()
    {
        while (true)
        {
            // Shuffle Spawning Points
            spawningVisitors = Shuffle(spawningVisitors);

            // Spawn Wave
            int numberVisitorTemp = Random.Range(numberSpawnMin, numberSpawnMax);
            SpawnWave(numberVisitorTemp);

            yield return new WaitForSeconds(spawningTime);
        }
    }

    void SpawnWave(int numberVisitors)
    {

        GuideMovement guideTemp = Instantiate(guidePrefab, spawningGuide.transform.position, Quaternion.identity);
        for (int i = 0; i < numberVisitors; i++)
        {
            Visitor visitorTemp = Instantiate(visitorPrefab, spawningVisitors[i].transform.position, Quaternion.identity);
            visitorTemp.guideToFollow = guideTemp.transform;
        }
    }

    // Method in order to Shuffle detinations array
    Transform[] Shuffle(Transform[] transformArray)
    {
        for (int t = 0; t < transformArray.Length; t++)
        {
            Transform tmp = transformArray[t];
            int r = Random.Range(t, transformArray.Length);
            transformArray[t] = transformArray[r];
            transformArray[r] = tmp;
        }

        return transformArray;
    }
}
