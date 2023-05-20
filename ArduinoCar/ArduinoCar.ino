int carRotation=0;
//int carSpeed=0;

//ultrasonic sensor
const int TrgPin = 7;
const int EcoPin = 6;
int dist;

int lightValue; //light sensor----------

const int vibrationMotorPin = 9;  // vibration---------------------
void setup()
{
    Serial.begin(9600);
    //pinMode(A0,INPUT);
    pinMode(A1,INPUT);


//ultrasonic 

     pinMode(TrgPin, OUTPUT);  //TrgPin input

    pinMode(EcoPin, INPUT);// EcoPin input


    const int LightSensorPin = A2;     //light sensor------------

    pinMode(vibrationMotorPin, OUTPUT);// vibration---------------------
    
}


void loop()
{
    lightValue = analogRead(A2);  //light sensor-------
    int mappedLight = map(lightValue, 0, 1023, 0, 100);  //light sensor-------


  
   //carSpeed = analogRead(A0);
   carRotation = analogRead(A1);
   //Serial.print(map(carSpeed,0,1023,-100,100));
   //Serial.print(",");
   //Serial.println(map(carRotation,0,1023,-100,100));
   
    //delay(50);
    // 延迟5秒


    digitalWrite(TrgPin, LOW);

    delayMicroseconds(2);

    digitalWrite(TrgPin, HIGH);  

    delayMicroseconds(10);   // delay
    digitalWrite(TrgPin, LOW);

    dist = pulseIn(EcoPin, HIGH) / 58.00;  // make it distance

    
     //ultrasonic map
     
     int mappedDist = map(dist, 0, 100, 0, 100);  // Map the dist value to the range of 0-100

    // Limit the mapped value to the range of 0-100
    if (mappedDist < 0)
    {
        mappedDist = 0;
    }
    else if (mappedDist > 100)
    {
        mappedDist = 50;
    }



    Serial.print(mappedDist);
     //Serial.print(map(dist, 0, 100, 0, 100));
    //Serial.print(dist); //print
     Serial.print(",");
   //Serial.println(map(carRotation,0,1023,-100,100));//-------

   Serial.print(map(carRotation,0,1023,-100,100));//
   Serial.print(",");                    //
    Serial.println(mappedLight);          //light sensor-------
    
    delay(100);

    
    
    // vibration----------------------------------------------------
    if (Serial.available() > 0)
    {
        int hitStatus = Serial.read() - '0';
        if (hitStatus == 1)
        {
            digitalWrite(vibrationMotorPin, HIGH);  // Activate the vibration motor
            delay(500);  // Vibrate for 1 second
            digitalWrite(vibrationMotorPin, LOW);  // Turn off the vibration motor
        }
    }
    // vibration----------------------------------------------------


    
}
