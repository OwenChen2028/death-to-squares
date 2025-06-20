using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Kill();
        }
        
        if (other.name != "Ball(Clone)")
        {
            counter += 1;
            if (counter == 2)
            {
                Destroy(gameObject);
            }
        }
    }
}
