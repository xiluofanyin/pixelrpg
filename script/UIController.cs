using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    public TMP_Text healthText,staText,coinText,bossName ;
    public Slider healthSlider,staSlider,bossSlider;
    public string mainMeunuscene;
    public GameObject pausescreen;
    public GameObject lackscreen;
    public GameObject deathScreen;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        uphealth();
        upsta();
        upcoin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void uphealth()
    {
        healthSlider.maxValue = PlayerhealthController.instance.maxHealth;
        healthSlider.value = PlayerhealthController.instance.currentHealth;
        healthText.text = "HP" + PlayerhealthController.instance.currentHealth + "/" + PlayerhealthController.instance.maxHealth;
    }
    public void upsta()
    {
        staSlider.maxValue = PlayerController.instance.totalStamina;
        staSlider.value = PlayerController.instance.currstamin;
        staText.text = "SP" + PlayerController.instance.currstamin + "/" + PlayerController.instance.totalStamina;
    }
    public void upcoin()
    {
        coinText.text = "coin" + GameManager.instance.curCoin;
    }
    public void gotomain()
    {
        SceneManager.LoadScene(mainMeunuscene);
        Time.timeScale = 1f;
    }
    public void re()    
    {
            GameManager.instance.pause();
    }

    public void exit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();  
    }
}
