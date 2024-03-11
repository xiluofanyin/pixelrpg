using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelStartmoving : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.levelmoveing();
        SaveController.instance.save.curScene=SceneManager.GetActiveScene().name;
        SaveController.instance.save.startPosition=PlayerController .instance.transform.position;
        SaveController.instance.saveInfo(); 
    }

    // Update is called once per frame
   
}
