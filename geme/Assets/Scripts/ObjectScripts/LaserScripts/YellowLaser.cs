using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowLaser : MonoBehaviour
{
    private BoxCollider2D yellow_box;
    // Start is called before the first frame update
    void Start()
    {
        yellow_box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ColorManager.yellow_disable)
        {
            yellow_box.enabled = false;
        }
        else
        {
            yellow_box.enabled = true;
        }
    }
}
