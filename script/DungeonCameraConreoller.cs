using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCameraConreoller : MonoBehaviour
{
    public static DungeonCameraConreoller Instance;
    public float movespeed;
    public Vector3 targetpoint;
    public bool inBossroom;
    private Vector3 limup, limlow;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        targetpoint.z = transform.position.z;   
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (inBossroom)
        {
            targetpoint.y=Mathf.Clamp(PlayerController.instance.transform.position.y,limlow.y, limup.y);
        }
            transform.position = Vector3.MoveTowards(transform.position, targetpoint, movespeed * Time.deltaTime);
        
    }
    public void actbossroom(Vector3 up,Vector3 low)
    {
        inBossroom = true;  
        limup = up;
        limlow = low;
    }
}
