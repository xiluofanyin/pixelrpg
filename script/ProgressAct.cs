using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressAct : MonoBehaviour
{
    public string preogress;
    public bool actMark;
    // Start is called before the first frame update
    void Start()
    {
        bool isMark=SaveController.instance.CheckProgress(preogress);
        if(actMark)
        {
            gameObject.SetActive(!isMark);
        }
        else
        {
            gameObject.SetActive(isMark);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
