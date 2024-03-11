using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybullet : MonoBehaviour
{
    public float movespeed;
    public int damage;
    private Vector3 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        moveDir = PlayerController.instance.transform.position - transform.position;
        moveDir.Normalize();    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position +=moveDir*movespeed*Time.deltaTime;  
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            PlayerhealthController.instance.damagePlayer(damage);
        }
        Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);    
    }
}
