using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    // Start is called before the first frame update
    // WHITE : -1 : RED : 0 , YELLOW : 1 , GREEN : 2, BLUE : 3 , ORANGE : 4 
    public GameObject PlayerPrefab;
    private GameObject ClonePrefab;

    private SpriteRenderer playerSprite;
    private SpriteRenderer cloneSprite;

    public static int color_code = -1;
    public static int clone_spawn = 0;
    public static bool yellow_disable = false;
    public static bool red_disable = false;
    public static bool green_disable = false;
    public static bool blue_disable = false;
    public static bool orange_disable = false;
    void Start()
    {
        playerSprite = PlayerPrefab.GetComponent<SpriteRenderer>(); 
    }
    private void RespawnInstance()
    {
        cloneSprite = GameObject.FindGameObjectWithTag("Respawn").GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if(clone_spawn == 1)
        {
            RespawnInstance();
            if (color_code == -1)
            {
                playerSprite.color = Color.white;
                cloneSprite.color = Color.white;
            }
            else if (color_code == 0)
            {
                playerSprite.color = Color.red;
                cloneSprite.color = Color.red;
            }
            else if (color_code == 1)
            {
                playerSprite.color = Color.yellow;
                cloneSprite.color = Color.yellow;
            }
            else if (color_code == 2)
            {
                playerSprite.color = Color.green;
                cloneSprite.color = Color.green;
            }
            else if (color_code == 3)
            {
                playerSprite.color = Color.cyan;
                cloneSprite.color = Color.cyan;
            }
            else if (color_code == 4)
            {
                playerSprite.color = Color.HSVToRGB(0.3f,1,1);
                cloneSprite.color = Color.HSVToRGB(0.3f,1,1);
            }
        }
        else
        {
            if (color_code == -1)
            {
                playerSprite.color = Color.white;
            }
            else if (color_code == 0)
            {
                playerSprite.color = Color.red;
            }
            else if (color_code == 1)
            {
                playerSprite.color = Color.yellow;
            }
            else if (color_code == 2)
            {
                playerSprite.color = Color.green;
                           }
            else if (color_code == 3)
            {
                playerSprite.color = Color.cyan;               
            }
            else if (color_code == 4)
            {
                playerSprite.color = Color.HSVToRGB(0.3f, 1, 1);
            }
        }
    }
}
