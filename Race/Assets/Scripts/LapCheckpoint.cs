using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapCheckpoint : MonoBehaviour
{
    public Text checkPoint;
    public int Number;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CarLap>())
        {
            CarLap car = other.GetComponent<CarLap>();

            if (car.CheckpointNumber == Number + 1 || car.CheckpointNumber == Number - 1)
            {
                car.CheckpointNumber = Number;
            }
            checkPoint.text = "Checkpoint: " + Number;
        }
    }

}
