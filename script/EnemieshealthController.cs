using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieshealthController : MonoBehaviour
{
    public int currentHealth;
    public GameObject deathEffect;
    private EnemyController theEC;
    public GameObject healrhdrop,coindrop;
    public float chance;
    // Start is called before the first frame update
    void Start()
    {
        theEC = GetComponent<EnemyController>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(int damageAcc)
    {
        currentHealth -= damageAcc;
        if(currentHealth <= 0)
        {
            if(deathEffect != null)
            {
                Instantiate(deathEffect,transform.position,transform.rotation);
            }
            Destroy(gameObject);
            if(Random.Range(0f,100f)<chance&&healrhdrop != null)
            {
                Instantiate(healrhdrop,transform.position,transform.rotation);  
            }
            if (Random.Range(0f, 100f) < chance && coindrop != null)
            {
                Instantiate(coindrop , transform.position, transform.rotation);
            }
            AudioController.instance.play(4);
        }
        AudioController.instance.play(7);
        theEC.knockback(PlayerController.instance.transform.position);
    }
}
