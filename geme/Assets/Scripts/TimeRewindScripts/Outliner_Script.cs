using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outliner_Script : MonoBehaviour
{
    public GameObject player;
    private Transform Border;
    private PointInTime point;
    private TimeBody timebody;
    private int count;
    void Start()
    {
        Border = GetComponent<Transform>();
        timebody = player.GetComponent<TimeBody>();
    }

    // Update is called once per frame
    private void Update()
    {
        count = timebody.pointsInTime.Count;
        if (count > 0)
        {
            point = timebody.pointsInTime[count - 1];
            Border.SetPositionAndRotation(point.position, point.rotation);
        }
    }
}
