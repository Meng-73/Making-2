using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;


public class CarController : MonoBehaviour
{
     
     
      //////////////////////this is ultrasonic sensor


    public float movementSpeed = 5f;  // Speed of cube movement
    public float distanceThreshold = 50f;  // Distance threshold to trigger movement

    SerialPort sp = new SerialPort("COM3", 9600);  // Set the correct COM port


    // Start is called before the first frame update
    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (sp.IsOpen)
        {
            try
            {
                int distance = int.Parse(sp.ReadLine());  // Read the distance value from Arduino

                if (distance > distanceThreshold)
                {
                    // Move the cube to the right
                    transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
                }
                else
                {
                    // Move the cube to the left
                    transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
                }
            }
            catch (System.Exception)
            {
                // Handle the exception
            }
        }
    }
}
