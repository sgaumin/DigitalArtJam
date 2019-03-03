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

    private GameObject player;

    [SerializeField] private float soundDistThreshold = 30f;

    private void Start()
    {
        captureWordSlider = GameObject.FindGameObjectWithTag("WordSlider").GetComponent<Slider>();
        collider.radius = guideValues.talkRange;
        guideMovement = GetComponent<GuideMovement>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private IEnumerator StartCollectingWord()
    {
        collectingWord = true;
        captureWordProgress = 0f;

        AkSoundEngine.PostEvent("Play_Guide_Voice_In", gameObject);
        AkSoundEngine.SetState("SoundEffects", "In");
        while (captureWordProgress <= 100f)
        {
            captureWordProgress += Time.deltaTime * guideValues.captureSpeed;
            yield return null;
        }

        // Word collected, yey!
        collectingWord = false;
        AkSoundEngine.SetSwitch("Fail_Chek_Zone", "Check", this.gameObject);
        //  AkSoundEngine.SetState ("SoundEffects", "Out");
        AkSoundEngine.PostEvent("Play_Guide_Voice_Out_02", gameObject);
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
            captureWordProgress = 0f;
        {
            // son feedback bad
        }

        collectingWord = false;
        captureWordProgress = 0f;
        yield return null;
    }

    private void Update()
    {
        float distance = (player.transform.position - transform.position).magnitude;
        AkSoundEngine.SetRTPCValue("Dist_Guide_Player", Mathf.Min(distance, soundDistThreshold), gameObject);
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