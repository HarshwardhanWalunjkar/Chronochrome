using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLaser : MonoBehaviour
{
    private BoxCollider2D green_box;
    // Start is called before the first frame update
    void Start()
    {
        green_box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ColorManager.green_disable) 
        { 
            green_box.enabled = false;
        }
        else
        {
            green_box.enabled = true;
        }
    }
}
