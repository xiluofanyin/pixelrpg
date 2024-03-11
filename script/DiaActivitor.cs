using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaActivitor : MonoBehaviour
{
    public string[] line;
    private bool canact;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canact&&Input.GetMouseButtonDown(0)&&!DialogCVontroller.Instance.dialogbox.activeInHierarchy)
        {
            DialogCVontroller.Instance.showdia(line,true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Player")
        {
            canact = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canact = false;
        }
    }
}
