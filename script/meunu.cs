using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class meunu : MonoBehaviour
{
    public string startscene;
    public GameObject continnueBu;
    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
            GameManager.instance = null;    
        }
        if(PlayerController.instance != null)
        {
            Destroy(PlayerController.instance.gameObject);  
            PlayerController.instance = null;   
        }
        if(SaveController.instance.save.hasBegun)
        {
            continnueBu.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startgame()
    {
        SceneManager.LoadScene(startscene);
        SaveController.instance.restsave();
        SaveController.instance.save.hasBegun = true;   
    }
    public void exitgame()
    {
        Debug.Log("Quitting Game");
        Application.Quit(); 
    }
    public void Continue()
    {
        SceneManager.LoadScene(SaveController.instance.save.curScene);
    }
}
