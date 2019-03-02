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
        captureWordProgress = 0f;
        while (captureWordProgress <= 100f)
        {
            captureWordProgress += Time.deltaTime * speed;
            yield return null;
        }

        // Word collected, yey!
        PlayerManager.instance.nbWordsCollected++;
        captureWordProgress = 0f;

        yield return null;
    }

    private void LateUpdate()
    {
        captureWordSlider.value = captureWordProgress;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        StartCoroutine("StartCollectingWord");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        captureWordProgress = 0f;
        StopCoroutine("StartCollectingWord");
    }
}