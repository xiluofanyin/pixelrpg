using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPick : MonoBehaviour
{
    public int coinvalue;
    public float waittime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (waittime > 0)
        {
            waittime -= Time.deltaTime;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player"&&waittime<=0)
        {
            GameManager.instance.coin(coinvalue);
            Destroy (gameObject);
            AudioController.instance.play(3); 
        }
    }
}
