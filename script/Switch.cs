using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool inrange;
    public GameObject obtoStop;
    private bool isOn;
    public SpriteRenderer switchof;
    public Sprite off, on;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(inrange)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isOn=!isOn;
                if(isOn)
                {
                    switchof.sprite = on;
                }
                else
                {
                    switchof.sprite = off;
                }
                obtoStop.SetActive(!isOn);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            inrange = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inrange = false;
        }
    }
}
