using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthpick : MonoBehaviour
{
    public int health;
    public float timeback;
    public float waittime = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        if(timeback>0)
        Destroy(gameObject,timeback);
    }

    // Update is called once per frame
    void Update()
    {
        if(waittime>0)
        {
            waittime -= Time.deltaTime; 
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player"&&waittime <=0)
        {
            PlayerhealthController.instance.healthup(health); 
            Destroy(gameObject);
            AudioController.instance.play(6);
        }
    }
}
