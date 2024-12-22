using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    public Sprite[] buttonsprites;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = buttonsprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchSprite()
    {
        index = (index + 1)%2;
        GetComponent<Image>().sprite = buttonsprites[index];
    }
}
