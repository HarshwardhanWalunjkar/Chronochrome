using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneTriggerSensor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "RedTrigger")
        {
            ColorManager.color_code = 0;
        }
        else if (collision.gameObject.tag == "YellowTrigger")
        {
            ColorManager.color_code = 1;
        }
        else if (collision.gameObject.tag == "GreenTrigger")
        {
            ColorManager.color_code = 2;
        }
        else if (collision.gameObject.tag == "BlueTrigger")
        {
            ColorManager.color_code = 3;
        }
        else if (collision.gameObject.tag == "OrangeTrigger")
        {
            ColorManager.color_code = 4;
        }
        else if (collision.gameObject.tag == "RedLaserTrigger")
        {
            if (ColorManager.color_code == 0)
            {
                ColorManager.red_disable = true;
            }
            else
            {
                ColorManager.red_disable = false;
            }
        }
        else if (collision.gameObject.tag == "YellowLaserTrigger")
        {
            if (ColorManager.color_code == 1)
            {
                ColorManager.yellow_disable = true;
            }
            else
            {
                ColorManager.yellow_disable = false;
            }
        }
        else if (collision.gameObject.tag == "GreenLaserTrigger")
        {
            if (ColorManager.color_code == 2)
            {
                ColorManager.green_disable = true;
            }
            else
            {
                ColorManager.green_disable = false;
            }
        }
        else if (collision.gameObject.tag == "BlueLaserTrigger")
        {
            if (ColorManager.color_code == 3)
            {
                ColorManager.blue_disable = true;
            }
            else
            {
                ColorManager.blue_disable = false;
            }
        }
        else if (collision.gameObject.tag == "OrangeLaserTrigger")
        {
            if (ColorManager.color_code == 4)
            {
                ColorManager.orange_disable = true;
            }
            else
            {
                ColorManager.orange_disable = false;
            }
        }
    }
}
