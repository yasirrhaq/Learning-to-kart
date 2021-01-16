using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrackManager : MonoBehaviour
{
    public string trackName;
    public int lapTotal;
    public Collider[] checkpoints;
    public GameObject minuteDisplay;
    public GameObject secondDisplay;
    public GameObject milliDisplay;
    public GameObject currentLapDisplay;
    public GameObject totalLapDisplay;
    private Lap lap;

    private LapTimeManager lapTime;

    // Start is called before the first frame update
    void Start()
    {
        lap = GameObject.FindWithTag("Player").GetComponent<Lap>();
        lapTime = GameObject.FindWithTag("Time Manager").GetComponent<LapTimeManager>();
        totalLapDisplay.GetComponent<Text>().text = lapTotal.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (GameObject.FindWithTag("Player").GetComponent<Lap>())
        {
            Lap lap = GameObject.FindWithTag("Player").GetComponent<Lap>();

            if (lap.checkpointIndex == checkpoints.Length - 1)
            {
                lap.checkpointIndex = 0;
                if (lap.lapNumber < lapTotal)
                {
                    lap.lapNumber++;
                    currentLapDisplay.GetComponent<Text>().text = lap.lapNumber.ToString();
                    lapTime.LapRecordConverter(lap.lapNumber, LapTimeManager.minuteCount, LapTimeManager.secondCount, LapTimeManager.milliCount/1000);
                }

            }

        }
    }

}
