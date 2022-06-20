using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSV : MonoBehaviour
{
    [Header("CSV's to read from")]
    public TextAsset[] CSVToRead;

    //Benchpress data
    private double accX, accY, accZ;
    private double gyroX, gyroY, gyroZ;
    private double acceleration;
    private double force;

    public double Force { get => force;  }

    private void Awake()
    {
        ReadFromCSV("");
    }
    private void ReadFromCSV(string path)
    {
        // Read all the lines
        string[] lines = CSVToRead[0].text.Split("\n"[0]);

        for (int i = 0; i < lines.Length; i++)
        {
            // This is to get every thing that is comma separated
            string[] parts = lines[i].Split(","[0]);

            accX += double.Parse(parts[0].ToString());
            accY += double.Parse(parts[1].ToString());
            accZ += double.Parse(parts[2].ToString());
            gyroX += double.Parse(parts[3].ToString());
            gyroY += double.Parse(parts[4].ToString());
            gyroZ += double.Parse(parts[5].ToString());
        }
        //Todo: Fix values
        acceleration = accX + accY + accZ;
        force = 70*acceleration;
    }
}
