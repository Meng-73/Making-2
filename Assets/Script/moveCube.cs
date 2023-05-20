using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class moveCube : MonoBehaviour
{

    SerialPort sp= new SerialPort("COM3",9600);

    // Start is called before the first frame update
    void Start()
    {
        sp.Open();
        sp.ReadTimeout=100;

    }

    // Update is called once per frame
    void Update()
    {
        if (sp.IsOpen){
            try{
                if(sp.ReadByte()==1){
                    transform.Translate(Vector3.left * Time.deltaTime * 5);

                }
                 if(sp.ReadByte()==2){
                    transform.Translate(Vector3.right * Time.deltaTime * 5);
                    
                }
            }
            catch(System.Exception){

            }
        }
    }
}
