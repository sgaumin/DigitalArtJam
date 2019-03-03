using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GuideTalking : MonoBehaviour
{
    public float captureWordProgress = 0f;

    private Slider captureWordSlider;
    private bool collectingWord = false;

    public GuideManagerScriptableObject guideValues;

    public CapsuleCollider collider;

    private GuideMovement guideMovement;
    
    private void Start()
    {
        captureWordSlider = GameObject.FindGameObjectWithTag("WordSlider").GetComponent<Slider>();
        collider.radius = guideValues.talkRange;
        guideMovement = GetComponent<GuideMovement>();
    }

    private IEnumerator StartCollectingWord()
    {
        collectingWord = true;
        captureWordProgress = 0f;

        // Start word Detection
        while (captureWordProgress <= 100f)
        {
            captureWordProgress += Time.deltaTime * guideValues.captureSpeed;
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
            captureWordProgress -= Time.deltaTime * guideValues.resetSpeed;
            yield return null;
        }

        if (!collectingWord)
        {
            // son feedback bad
        }

        collectingWord = false;
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

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (guideMovement.guideState == GuideState.Talk && !collectingWord)
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