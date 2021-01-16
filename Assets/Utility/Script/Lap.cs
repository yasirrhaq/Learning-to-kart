using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lap : MonoBehaviour
{
    public int lapNumber;
    public int checkpointIndex;
    // Start is called before the first frame update
    void Start()
    {
        lapNumber = 1;
        checkpointIndex = 0;
    }
}
