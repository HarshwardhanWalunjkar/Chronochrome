using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline_Replay_Script : MonoBehaviour
{
    public GameObject player;
    private Transform Border;
    private PointInTime point;
    private ReplayBody replay;
    private int count;
    void Start()
    {
        Border = GetComponent<Transform>();
        replay = player.GetComponent<ReplayBody>();
    }

    // Update is called once per frame
    private void Update()
    {
        count = replay.pointsInTimeReplay.Count;
        if (count > 0)
        {
            point = replay.pointsInTimeReplay[count-1];
            Border.SetPositionAndRotation(new Vector2(point.position.x+0.06f, point.position.y+0.054f), point.rotation);
        }
    }
}
