using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwordGet : MonoBehaviour
{
    public GameObject door;
    public int newDamage,swordJudge;
    public string[] pickdia;
    public string win;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        {
            gameObject.SetActive(false);
            if(door != null)
            {
                door.SetActive(false);  
            }
            PlayerController.instance.upGradeSword(newDamage,swordJudge);
        }
        if(pickdia.Length > 0)
        {
            DialogCVontroller.Instance.showdia(pickdia,false);
        }
       
    }
}
