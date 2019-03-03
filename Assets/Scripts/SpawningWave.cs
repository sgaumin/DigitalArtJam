using System.Collections;
using UnityEngine;
using TMPro;

public class SpawningWave : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GuideMovement guidePrefab;
    [SerializeField] private Visitor[] visitorPrefab;

    [Header("Parameters")]
    [SerializeField] private float hourDuration;
    [SerializeField] private float[] nbSpawnPerHours;

    [Space]
    [SerializeField] private int numberVisitorSpawnMin;
    [SerializeField] private int numberVisitorSpawnMax;

    [Header("Spawning points")]
    [SerializeField] private Transform[] spawningPointsVisitor;
    [SerializeField] private Transform spawningPointsGuide;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI hourText;

    private int hours;
    private int indexHour;

    void Start()
    {
        // Hours UI
        hours = 10;
        indexHour = 0;
        UpdateHour();

        StartCoroutine(SpawningWaves());
    }

    IEnumerator SpawningWaves()
    {
        while (true)
        {
            for (int i = 0; i < nbSpawnPerHours[indexHour]; i++)
            {
                // Shuffle Spawning Points
                spawningPointsVisitor = Shuffle(spawningPointsVisitor);

                // Spawn Wave
                int numberVisitorTemp = Random.Range(numberVisitorSpawnMin, numberVisitorSpawnMax);
                SpawnWave(numberVisitorTemp);

                yield return new WaitForSeconds(hourDuration / nbSpawnPerHours[indexHour]);
            }

            indexHour++;
            UpdateHour();
        }
    }

    void SpawnWave(int numberVisitors)
    {

        GuideMovement guideTemp = Instantiate(guidePrefab, spawningPointsGuide.transform.position, Quaternion.Euler(new Vector3(0f,180f,0f)));
        guideTemp.transform.SetParent(transform);

        for (int i = 0; i < numberVisitors; i++)
        {
            Visitor visitorTemp = Instantiate(visitorPrefab[Random.Range(0, visitorPrefab.Length)], spawningPointsVisitor[i].transform.position, Quaternion.Euler(new Vector3(0f, 180f, 0f)));
            visitorTemp.transform.SetParent(transform);
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

    // Update hour time text
    void UpdateHour()
    {
        hours++;
        hourText.text = hours.ToString() + ":00";
    }
}
