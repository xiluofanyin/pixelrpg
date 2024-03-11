using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaActivatior : MonoBehaviour
{
    private BoxCollider2D aeraBox;
    public GameObject[] enemies;
    public List<GameObject >clonenemies=new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        aeraBox = GetComponent<BoxCollider2D>();
        foreach(GameObject enemy in enemies)
        {
            enemy.SetActive(false); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void startenemies()
    {
        foreach(GameObject enemy in enemies)
        {
            GameObject newEnemy= Instantiate(enemy,enemy.transform.position,enemy.transform.rotation);
            newEnemy.SetActive(true);
            clonenemies.Add(newEnemy);
        }
    }
    private void clearenemies()
    {
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        clonenemies.Clear();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
         if(other.tag=="Player")
        {
            CameraController.instance.aeraBox = aeraBox;
            startenemies();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           if(PlayerhealthController.instance.currentHealth>0)
           clearenemies();
        }
    } 
}
