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

    public float alarmValue = 0f;
    public bool alarmStateGuide = false;
    public bool alarmStateVisitor = false;

    public bool lowerAlarmRunning = false;
    public bool guideAlarmRunning = false;
    public bool visitorAlarmRunning = false;

    public int visitorsNb = 0;

    public bool alertMode = false;

    private List<Guid> visitorsAlarm = new List<Guid>();

    public int nbWordsCollected = 0;

    public AlarmManagerScriptableObject alarmValues;
    
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // Stop Alarm
        if (!alarmStateGuide && !alarmStateVisitor)
        {
            guideAlarmRunning = false;
            visitorAlarmRunning = false;
            StopCoroutine("AddAlarmFromGuide");
            StopCoroutine("AddAlarmFromVisitors");

            if (!lowerAlarmRunning && alarmValue > 0.0f)
            {
                StartCoroutine(LowerAlarm());
            }
        }
        else
        {
            lowerAlarmRunning = false;
            StopCoroutine("LowerAlarm");

            // Start Guide Alarm
            if (alarmStateGuide)
            {
                if (!guideAlarmRunning && alarmValue <= maxAlarmValue)
                {
                    StartCoroutine(AddAlarmFromGuide());
                }
            }
            else
            {
                // Stop Guide Alarm
                guideAlarmRunning = false;
                StopCoroutine("AddAlarmFromGuide");
            }

            // Start Visitor Alarm
            if (alarmStateVisitor)
            {
                if (!visitorAlarmRunning && alarmValue <= maxAlarmValue)
                {
                    StartCoroutine(AddAlarmFromVisitors());
                }
            }
            else
            {
                // Stop Visitor Alarm
                visitorAlarmRunning = false;
                StopCoroutine("AddAlarmFromVisitors");
            }
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
        alertMode = alarmValue > alarmValues.alertThreeshold;
    }

    private IEnumerator LowerAlarm()
    {
        lowerAlarmRunning = true;
        while (alarmValue >= 0.0f && lowerAlarmRunning)
        {
            alarmValue -= Time.deltaTime * alarmValues.lowerAlarmSpeed;
            yield return null;
        }

        alarmValue = 0f;
        lowerAlarmRunning = false;
    }

    private IEnumerator AddAlarmFromGuide()
    {
        guideAlarmRunning = true;
        while (alarmValue <= maxAlarmValue && guideAlarmRunning)
        {
            alarmValue += Time.deltaTime * alarmValues.guideAlarmSpeed;
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
            if (visitorsNb < alarmValues.visitorsTier2)
                speed = alarmValues.visitorsAlarmSpeedTier1;
            else if (visitorsNb < alarmValues.visitorsTier3)
                speed = alarmValues.visitorsAlarmSpeedTier2;
            else
                speed = alarmValues.visitorsAlarmSpeedTier3;

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