using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(60 * Time.deltaTime,0,0);
        //transform.Rotate(Vector3.forward *90*Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {

          FindObjectOfType<AudioManager>().PlaySound("PickUpCoin");
          PlayerManager.numberOfCoins +=1;


          //Debug.Log("1");
          Destroy(gameObject);

        }
    }
}
