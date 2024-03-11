using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public string sceneload;
    public Vector3 exitlocation;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        {
            PlayerController.instance.transform.position = exitlocation;
            PlayerController.instance.theRB.velocity = Vector2.zero;
            PlayerController.instance.canmove = false;  
            UIController.Instance.lackscreen.SetActive(true);  
            SceneManager.LoadScene(sceneload);
        }
    }
}
