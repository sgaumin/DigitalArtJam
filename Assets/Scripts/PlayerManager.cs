using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Slider AlarmSlider;
    public static PlayerManager instance { get; private set; }

    private float maxAlarmValue = 100f;

    [SerializeField] private float lowerAlarmSpeed = 0.1f;
    [SerializeField] private float guideAlarmSpeed = 0.05f;
    [SerializeField] private float visitorsAlarmSpeedTier1 = 1f;
    [SerializeField] private float visitorsAlarmSpeedTier2 = 5f;
    [SerializeField] private float visitorsAlarmSpeedTier3 = 20f;
    [SerializeField] private int visitorsTier2 = 4;
    [SerializeField] private int visitorsTier3 = 8;

    public float alarmValue = 0f;
    public bool alarmStateGuide = false;
    public bool alarmStateVisitor = false;

    public bool lowerAlarmRunning = false;
    public bool guideAlarmRunning = false;
    public bool visitorAlarmRunning = false;

    public int visitorsNb = 0;

    private List<Guid> visitorsAlarm = new List<Guid>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (alarmStateGuide || alarmStateVisitor)
        {
            lowerAlarmRunning = false;
            StopCoroutine("LowerAlarm");

            // Start Guide Alarm
            if (alarmStateGuide)
            {
                if (!guideAlarmRunning && alarmValue <= maxAlarmValue)
                {
                    lowerAlarmRunning = false;
                    StartCoroutine(AddAlarmFromGuide());
                }
            }
            // Start Visitor Alarm
            else if (alarmStateVisitor)
            {
                if (!visitorAlarmRunning && alarmValue <= maxAlarmValue)
                {
                    lowerAlarmRunning = false;
                    StartCoroutine(AddAlarmFromVisitors());
                }
            }
        }
        // Stop Alarm
        else if (!lowerAlarmRunning && alarmValue >= 0.0f)
        {
            StopCoroutine("AddAlarmFromGuide");
            StopCoroutine("AddAlarmFromVisitors");
            StartCoroutine(LowerAlarm());
        }

        // Stop Guide Alarm
        if (!alarmStateGuide)
        {
            guideAlarmRunning = false;
            StopCoroutine("AddAlarmFromGuide");
        }

        // Stop Visitor Alarm
        if (!alarmStateVisitor)
        {
            visitorAlarmRunning = false;
            StopCoroutine("AddAlarmFromVisitors");
        }

        // GAME OVER
        if (alarmValue >= maxAlarmValue)
        {
            // GameSystem.instance.gameState = GameState.GameOver;
        }
    }

    private void LateUpdate()
    {
        AlarmSlider.value = alarmValue;
    }

    private IEnumerator LowerAlarm()
    {
        lowerAlarmRunning = true;
        while (alarmValue >= 0.0f && lowerAlarmRunning)
        {
            alarmValue -= Time.deltaTime * lowerAlarmSpeed;
            yield return null;
        }

        lowerAlarmRunning = false;
    }

    private IEnumerator AddAlarmFromGuide()
    {
        guideAlarmRunning = true;
        while (alarmValue <= maxAlarmValue && guideAlarmRunning)
        {
            alarmValue += Time.deltaTime * guideAlarmSpeed;
            yield return null;
        }

        guideAlarmRunning = false;
    }

    private IEnumerator AddAlarmFromVisitors()
    {
        visitorAlarmRunning = true;
        while (alarmValue <= maxAlarmValue && visitorAlarmRunning)
        {
            float speed;
            visitorsNb = visitorsAlarm.Count;
            if (visitorsNb < visitorsTier2)
                speed = visitorsAlarmSpeedTier1;
            else if (visitorsNb < visitorsTier3)
                speed = visitorsAlarmSpeedTier2;
            else
                speed = visitorsAlarmSpeedTier3;

            alarmValue += Time.deltaTime * speed;
            yield return null;
        }

        visitorAlarmRunning = false;
    }

    public void OnSeenPlayer(string tag, Guid guid)
    {
        switch (tag)
        {
            case "Guide":
                alarmStateGuide = true;
                break;
            case "Visitor":
                if (!visitorsAlarm.Contains(guid))
                    visitorsAlarm.Add(guid);
                alarmStateVisitor = true;
                break;
        }
    }

    public void OnPlayerLost(string tag, Guid guid)
    {
        switch (tag)
        {
            case "Guide":
                alarmStateGuide = false;
                break;
            case "Visitor":
                visitorsAlarm.Remove(guid);
                if (visitorsAlarm.Count == 0)
                    alarmStateVisitor = false;
                break;
        }
    }
}