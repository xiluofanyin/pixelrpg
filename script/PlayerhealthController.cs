using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerhealthController : MonoBehaviour
{
    public static PlayerhealthController instance;
    public int currentHealth;
    public int maxHealth;
    public float invincibilityLength = 1f;
    private float invCounter;
    public GameObject deathepre;
    private void Awake()
    {
       if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = SaveController.instance.save.maxHP;
        currentHealth = maxHealth;
        UIController.Instance.uphealth();
    }

    // Update is called once per frame
    void Update()
    {
        if (invCounter > 0)
        {
            invCounter -= Time.deltaTime;
        }
    }
    public void damagePlayer(int damageAccount)
    {
        if (invCounter <= 0)
        {
            currentHealth -= damageAccount;
            invCounter = invincibilityLength;
            if(currentHealth<=0)
            {
                currentHealth = 0;
                gameObject.SetActive(false);
                Instantiate(deathepre, transform.position, transform.rotation);
                AudioController.instance.play(4);
                GameManager.instance.again();
            }
            UIController.Instance.uphealth();

            AudioController.instance.play(7);
        }
    }
    public void healthup(int num)
    {
        currentHealth += num;
        if(currentHealth >maxHealth)
        {
            currentHealth=maxHealth;    
        }
        UIController.Instance.uphealth();
    }
}
