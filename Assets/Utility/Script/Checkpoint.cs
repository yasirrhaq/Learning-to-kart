using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int index;

    void OnTriggerEnter(Collider other)
    {
        //if (other.GetComponent<Lap>())
        if (GameObject.FindWithTag("Player").GetComponent<Lap>())
        {
            Lap lap = GameObject.FindWithTag("Player").GetComponent<Lap>(); 

            if (lap.checkpointIndex == index + 1 || lap.checkpointIndex == index -1)
            {
                lap.checkpointIndex = index;

                //Debug.Log(index);
            }
        }
    }
}
