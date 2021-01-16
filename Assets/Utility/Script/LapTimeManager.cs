using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimeManager : MonoBehaviour
{
    public static int minuteCount;
    public static int secondCount;
    public static float milliCount;
    public static string milliDisplay;
    public GameObject minuteBox;
    public GameObject secondBox;
    public GameObject milliBox;
    public GameObject bestMinuteBox;
    public GameObject bestSecondBox;
    public GameObject bestMilliBox;

    public List<float> lapRecord;
    public List<float> sortedLapRecord;

    public float currentBest;

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        milliCount += Time.deltaTime * 10;
        milliDisplay = milliCount.ToString("F0");
        milliBox.GetComponent<Text>().text = "" + milliDisplay;

        if (milliCount >= 10)
        {
            milliCount = 0;
            secondCount += 1;
        }

        if (secondCount <= 9)
        {
            secondBox.GetComponent<Text>().text = "0" + secondCount + ".";
        }
        else
        {
            secondBox.GetComponent<Text>().text = "" + secondCount + ".";
        }

        if (secondCount >= 60)
        {
            secondCount = 0;
            minuteCount += 1;
        }

        if (minuteCount <= 9)
        {
            minuteBox.GetComponent<Text>().text = "0" + minuteCount + ":";
        }
        else
        {
            minuteBox.GetComponent<Text>().text = "" + minuteCount + ":";
        }
    }

    public void BestTime(int curLap, int minute, int second, float milli)
    {
        //Debug.Log("cur best: " + currentBest);
        if (lapRecord[curLap] <= currentBest)
        {
            milliDisplay = milliCount.ToString("F0");
            bestMilliBox.GetComponent<Text>().text = "" + milliDisplay;

            if (secondCount <= 9)
            {
                bestSecondBox.GetComponent<Text>().text = "0" + secondCount + ".";
            }
            else
            {
                bestSecondBox.GetComponent<Text>().text = "" + secondCount + ".";
            }


            if (minuteCount <= 9)
            {
                bestMinuteBox.GetComponent<Text>().text = "0" + minuteCount + ":";
            }
            else
            {
                bestMinuteBox.GetComponent<Text>().text = "" + minuteCount + ":";
            }
        }
    }

    private float RoundedMilli(float milli)
    {
        float roundedMilli = Mathf.Round(milli * 100f) / 100f;
        return roundedMilli;
    }

    private int TotalSecond(int minute, int second)
    {
        int totalSecond;

        if (minute != 0)
        {
            minute *= 60;
        }

        totalSecond = minute + second;

        return totalSecond;
    }
    public void LapRecordConverter(int curLap, int minute, int second, float milli)
    {
        int totalSecond = TotalSecond(minute, second);
        float totalLapTime;
        float roundedMilli = RoundedMilli(milli);
        //Debug.Log(totalSecond + " " + roundedMilli + " = " + (totalSecond + roundedMilli));

        totalLapTime = totalSecond + roundedMilli;

        lapRecord.Insert(curLap, totalLapTime);
        sortedLapRecord.Insert(curLap, totalLapTime);
        sortedLapRecord.Sort();

        currentBest = sortedLapRecord[0];
        // float lowestTime = lapRecord[0];

        // currentBest = lowestTime;

        // Debug.Log("currentLap " + curLap + " : " + lapRecord[curLap]);
    }
    public float getMilli()
    {
        float roundedMilli = Mathf.Round(milliCount * 100f) / 100f;
        return roundedMilli;
    }
}
