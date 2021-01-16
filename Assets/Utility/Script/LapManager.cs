using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KartGame.Skripsi
{
    public class LapManager : MonoBehaviour
    {
        public string trackName;
        public int lapTotal;
        public Collider[] checkpoints;
        public GameObject currentLapDisplay;
        public GameObject totalLapDisplay;
        private Lap lap;

        private LapTimeManager lapTime;

        void Start()
        {
            lap = GameObject.FindWithTag("Player").GetComponent<Lap>();
            lapTime = GameObject.FindWithTag("Time Manager").GetComponent<LapTimeManager>();
            totalLapDisplay.GetComponent<Text>().text = lapTotal.ToString();
        }
        void OnTriggerEnter(Collider other)
        {
            if (lap)
            {
                if (lap.checkpointIndex == checkpoints.Length - 1)
                {
                    lap.checkpointIndex = 0;
                    lapTime.LapRecordConverter(lap.lapNumber - 1, LapTimeManager.minuteCount, LapTimeManager.secondCount, LapTimeManager.milliCount / 10);
                    lapTime.BestTime(lap.lapNumber - 1, LapTimeManager.minuteCount, LapTimeManager.secondCount, LapTimeManager.milliCount / 10);

                    LapTimeManager.minuteCount = 0;
                    LapTimeManager.secondCount = 0;
                    LapTimeManager.milliCount = 0;
                    lap.lapNumber++;

                    if (lap.lapNumber <= lapTotal)
                    {
                        currentLapDisplay.GetComponent<Text>().text = lap.lapNumber.ToString();
                    }

                    if (lap.lapNumber > lapTotal)
                    {
                        Time.timeScale = 0;
                    }

                }
            }
        }
    }
}