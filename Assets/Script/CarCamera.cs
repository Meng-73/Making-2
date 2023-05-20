using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour
{
    public Transform target; // Reference to the car's transform
    public Vector3 offset = new Vector3(0f, 8f, -10f); // Offset position of the camera from the car

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired camera position
            Vector3 desiredPosition = target.position + offset;

            // Smoothly move the camera towards the desired position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);

            // Look at the car
            transform.LookAt(target);
        }
    }
}
