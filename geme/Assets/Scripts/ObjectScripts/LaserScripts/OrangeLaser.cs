using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeLaser : MonoBehaviour
{
    private BoxCollider2D orange_box;
    // Start is called before the first frame update
    void Start()
    {
        orange_box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ColorManager.orange_disable)
        {
            orange_box.enabled = false;
        }
        else
        {
            orange_box.enabled = true;
        }
    }
}
