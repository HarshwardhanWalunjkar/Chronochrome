using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedLaser : MonoBehaviour
{
    private BoxCollider2D red_box;
    // Start is called before the first frame update
    void Start()
    {
        red_box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ColorManager.red_disable)
        {
            red_box.enabled = false;
        }
        else
        {
            red_box.enabled = true;
        }
    }
}
