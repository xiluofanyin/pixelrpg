using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageEnemy : MonoBehaviour
{
    public int damage;
    public GameObject hitEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Hiteffection()
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Enemy")
        {
            other.GetComponent<EnemieshealthController>().takeDamage(damage);
            Hiteffection();
        }
        if(other.tag=="Breakable")
        {
            other.GetComponent<Breakableobject>().Break();
            Hiteffection();
        }
        if(other.tag=="Boss")
        {
            other.GetComponent<Bossweakpoint>().damgeBoss(damage);  
            Hiteffection();
        }
    }
}
