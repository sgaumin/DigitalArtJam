using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GuideTalking : MonoBehaviour
{
    [SerializeField] private float talkRange = 2f;
    [SerializeField] private float speed = 2f;

    public CapsuleCollider collider;

    public float captureWordProgress = 0f;
    public Slider captureWordSlider;

    private IEnumerator StartCollectingWord()
    {
        captureWordSlider.value = 0f;
        while (captureWordProgress <= 100f)
        {
            captureWordProgress += Time.deltaTime * speed;
            captureWordSlider.value = captureWordProgress;
            yield return null;
        }

        // Word collected, yey!
        PlayerManager.instance.nbWordsCollected++;

        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        StartCoroutine("StartCollectingWord");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        captureWordSlider.value = 0f;
        StopCoroutine("StartCollectingWord");
    }
}