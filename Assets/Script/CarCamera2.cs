using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera2 : MonoBehaviour
{
    public Transform target; // Reference to the car's transform
    public Vector3 offset ; // Offset position of the camera from the car

    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {   
        if (target != null) // Check if the target Transform is not null
        {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, offset.z + target.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, 5f);
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
