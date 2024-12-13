using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueLaser : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D blue_box;
    void Start()
    {
        blue_box = GetComponent<BoxCollider2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (ColorManager.blue_disable)
        {
            blue_box.enabled = false;
        }
        else
        {
            blue_box .enabled = true;
        }
    }
}
