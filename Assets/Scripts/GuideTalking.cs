using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GuideTalking : MonoBehaviour
{
    [SerializeField] private float talkRange = 2f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float resetSpeed = 20f;

    public float captureWordProgress = 0f;

    private Slider captureWordSlider;
    private bool collectingWord = false;

    private void Start()
    {
        captureWordSlider = GameObject.FindGameObjectWithTag("WordSlider").GetComponent<Slider>();
    }

    private IEnumerator StartCollectingWord()
    {
        collectingWord = true;
        captureWordProgress = 0f;

        // Start word Detection
        while (captureWordProgress <= 100f)
        {
            captureWordProgress += Time.deltaTime * speed;
            yield return null;
        }

        // Word collected, yey!
        collectingWord = false;
        // son check
        PlayerManager.instance.nbWordsCollected++;
        StartCoroutine("ResetCollectingWord");

        yield return null;
    }

    private IEnumerator ResetCollectingWord()
    {
        while (captureWordProgress > 0f && !collectingWord)
        {
            captureWordProgress -= Time.deltaTime * resetSpeed;
            yield return null;
        }
        
        if (!collectingWord)
            // son feedback bad
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
        collectingWord = false;
        StopCoroutine("StartCollectingWord");
        StartCoroutine("ResetCollectingWord");
    }
}