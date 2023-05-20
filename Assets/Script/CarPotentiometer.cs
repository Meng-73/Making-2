using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;


public class CarPotentiometer : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;

    public int hp = 20;
     // Flag to track if collision has occurred


     public float maxSpeed = 10f;  // Maximum speed of the car
    public float maxRotationAngle = 30f;  // Maximum rotation angle for the car
    // public float movementSpeed = 5f;  // Speed of the car movement
    //public float maxRotationAngle = 30f;  // Maximum rotation angle for the car

    SerialPort sp = new SerialPort("COM3", 9600);  // Set the correct COM port
    private float currentRotation = 0f;
    private float currentSpeed = 0f;




    public Light skyLight;//light sensor--------------------------------------------5.15
    int lightValue;//light sensor--------------------------------------------5.15
    public float minIntensity = 0.2f; //light sensor--------------------------------------------5.15
    public float maxIntensity = 1f;//light sensor--------------------------------------------5.15



    public GameObject HitPre; //explosion effect---------------------------------5.16
    public GameObject BombPre; //explosion effect---------------------------------5.16

    
    
    private bool vibrateMotor = false;//vibration------------------------------5.16





    // Start is called before the first frame update
    void Start()
    {
         sp.Open();
        sp.ReadTimeout = 100;

         controller = GetComponent<CharacterController>();
        Time.timeScale = 1.2f;

    }

    // Update is called once per frame
    void Update()
    {
         if (sp.IsOpen)
        {
            try
            {
                //if have ","
                 string[] sensorData = sp.ReadLine().Split(','); // Read the sensor data from Arduino and split it by comma

                        // Parse the values
                float ultrasonicValue = float.Parse(sensorData[0]);
                float potentiometerValue = float.Parse(sensorData[1]);


                lightValue = int.Parse(sensorData[2]); //light sensor--------------------------------------------5.15



                //if do not have ","
                //int potentiometerValue = int.Parse(sp.ReadLine());  // Read the potentiometer value from Arduino


                // Map the ultrasonic value to speed
                float targetSpeed = Mathf.Lerp(0f, maxSpeed, ultrasonicValue / 100f);
                
                if (ultrasonicValue <= 0)
                {
                    currentSpeed = 0f; // Stop the car if ultrasonic value is 0 or negative
                }
                else
                {
                    currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 5f);
                }
                  
                  
                  
                          // Map the potentiometer value to rotation angle
                float targetRotation = Mathf.Lerp(-maxRotationAngle, maxRotationAngle, (potentiometerValue + 100f) / 200f);
                currentRotation = Mathf.Lerp(currentRotation, targetRotation, Time.deltaTime * 10f);
                transform.rotation = Quaternion.Euler(0f, currentRotation, 0f);


                //light sensor-----------------------------------------------------------------------------5.15
                float lightIntensity = Mathf.Lerp(minIntensity, maxIntensity, lightValue / 10f);
                lightIntensity = Mathf.Pow(lightIntensity, 2f);
                skyLight.intensity = lightIntensity;
                //light sensor-------------------------------------------------------------5.15



                sp.Write(vibrateMotor ? "1" : "0");//vibration---------------------------------5.16
                vibrateMotor = false;//vibration---------------------------------5.16





            }

            



            catch (System.Exception)
            {
                // Handle the exception
            }
        }


        // // Move the car forward
        // transform.Translate(Vector3.forward * Time.deltaTime * movementSpeed);

         // Move the car forward based on the current speed
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);

        //controller.Move(Vector3.forward * Time.deltaTime * currentSpeed);

    }

    // private void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     if(hit.transform.tag == "Wall")
    //     {
    //         PlayerManager.gameOver = true;
    //         //FindObjectOfType<AudioManager>().PlaySound("GameOver");
    //     }
    // }


    private void OnCollisionEnter(Collision collision)
    {
        

        if(collision.collider.tag == "Wall")
        {
        //hp--;
        //PlayerManager.numberOfCoins -=1;

        //FindObjectOfType<AudioManager>().PlaySound("Hit");


        vibrateMotor = true; //vibration---------------------------------5.16
        

        if (PlayerManager.numberOfCoins <= 0)
        {
            // Die
            Instantiate (BombPre, transform.position, Quaternion.identity);  //explosion effect---------------------------------5.16
            //Destroy(gameObject);     //explosion effect---------------------------------5.16

            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
        else
        {
            // Handle collision without dying
            PlayerManager.numberOfCoins -=1;
            FindObjectOfType<AudioManager>().PlaySound("Hit");

            Instantiate (HitPre, collision.contacts[0].point,Quaternion.identity);  //explosion effect---------------------------------5.16
      
    
            
            //FindObjectOfType<AudioManager>().PlaySound("GameOver");4d6820  4C671D
        }
        }
    }



}



