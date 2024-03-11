using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : MonoBehaviour
{
    [TextArea]
    public string description;
    public int cost;
    private bool itemact;
    public bool isHPup, isstaup;
    public int amount;
    public bool removeafterbuy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(itemact)
        {
            if(Input.GetMouseButtonDown(0))
            {
                if(GameManager.instance.curCoin>=cost)
                {
                    GameManager.instance.curCoin-=cost;
                    UIController.Instance.upcoin();
                    if(isHPup)
                    {
                        PlayerhealthController.instance.maxHealth += amount;
                        PlayerhealthController.instance.currentHealth+=amount;
                        SaveController.instance.save.maxHP = PlayerhealthController.instance.maxHealth;
                        UIController.Instance.uphealth();
                    }
                    if(isstaup)
                    {
                        PlayerController.instance.totalStamina += amount;
                        PlayerController.instance.currstamin += amount;
                        SaveController.instance.save.sta= PlayerController.instance.totalStamina;   
                        UIController.Instance.upsta();
                    }
                    SaveController.instance.save.curCoin = GameManager.instance.curCoin;
                    if(removeafterbuy)
                    {
                        gameObject.SetActive(false);    
                    }
                    DialogCVontroller.Instance .dialogbox.SetActive(false); 
                    itemact = false;    
                }
                else
                {
                    DialogCVontroller.Instance.dialogtext.text = "you don have enough money.";
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        {
            itemact = true;
            DialogCVontroller.Instance.dialogbox.SetActive(true);
            DialogCVontroller.Instance.dialogtext.text = description;
            
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            itemact = false;
            DialogCVontroller.Instance.dialogbox.SetActive(false);

            
        }
    }
}
