using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeonroomact : MonoBehaviour
{
    public GameObject[] enemies;
    private List<GameObject> clonenemies = new List<GameObject>();
    public bool lockdoor;
    public GameObject[] Doors;
    private bool doorlocked,dontstartenemy;
    public bool isBoosroom;
    public Transform pointlow,pointup;
    public GameObject boss;
    private bool dontactBoss;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }
    private void Update()
    {
        if (doorlocked)
        {
            bool enemyfound = false;
            for(int i = 0;i<clonenemies.Count;i++)
            {
                if(clonenemies[i]!=null)
                {
                    enemyfound = true;  
                }
            }
            if(!enemyfound)
            {
                foreach (GameObject door in Doors)
                {
                    door.SetActive(false);
                }
                doorlocked = false;
                lockdoor= false;
                dontstartenemy = true;
            }
        }
    }
    // Update is called once per frame
    private void startenemies()
    {
        if (!dontstartenemy)
        {
            foreach (GameObject enemy in enemies)
            {
                GameObject newEnemy = Instantiate(enemy, enemy.transform.position, enemy.transform.rotation);
                newEnemy.SetActive(true);
                clonenemies.Add(newEnemy);
            }
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
            DungeonCameraConreoller.Instance.targetpoint = new Vector3(transform.position.x, transform.position.y, DungeonCameraConreoller.Instance.targetpoint.z);
            startenemies();
            if(lockdoor)
            {
                foreach(GameObject door in Doors)
                {
                    door.SetActive(true);   
                }
                doorlocked=true;
            }
            if(isBoosroom)
            {
                DungeonCameraConreoller.Instance.actbossroom(pointup.position,pointlow.position); 
                if(!dontactBoss)
                {
                    boss.SetActive(true);
                    dontactBoss = true;
                }
            }
            else
            {
                DungeonCameraConreoller.Instance.inBossroom = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (PlayerhealthController.instance.currentHealth > 0)
                clearenemies();
        }
    }
}
