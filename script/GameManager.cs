using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int curCoin;
    public bool diaact;
    public float waitForDead=1f,waitForagain=2f;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        curCoin = SaveController.instance.save.curCoin;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pause();
        }
    }
    public void coin(int addcoin)
    {
        curCoin += addcoin;
        UIController.Instance.upcoin();
        SaveController.instance.save.curCoin = curCoin;
    }
    public void pause()
    {
        if(!UIController.Instance.pausescreen.activeInHierarchy)
        {
            UIController.Instance.pausescreen.SetActive(true);
            Time.timeScale = 0f;
            PlayerController.instance.canmove = false;
        }
        else 
        {
            UIController.Instance.pausescreen.SetActive(false);
            Time.timeScale = 1f;
            PlayerController.instance.canmove=true;
        }
    }
    public void again()
    {
        StartCoroutine(againCo());
    }
    public IEnumerator againCo()
    {
        yield return new WaitForSeconds(waitForDead);
        UIController.Instance.deathScreen.SetActive(true); 
        yield return new WaitForSeconds(waitForagain);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        UIController.Instance.lackscreen.SetActive(true);   
        PlayerController.instance.Resetionagain();
    }
}
