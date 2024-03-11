using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogCVontroller : MonoBehaviour
{
    public static DialogCVontroller Instance;
    public GameObject dialogbox;
    public TMP_Text dialogtext;
    public string[] dialogline;
    public int curline;
    private bool justStart;
    private void Awake()
    {
        Instance = this;    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
            if (GameManager.instance.diaact)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if (!justStart)
                    {
                        curline++;
                        if (curline >= dialogline.Length)
                        {
                            dialogbox.SetActive(false);
                            GameManager.instance.diaact = false;    
                        }
                        else
                        {
                            dialogtext.text = dialogline[curline];
                        }
                    }
                    else
                    {
                        justStart = false;
                    }
                }
            }
        
    }
    public void showdia(string[] newline,bool shouldwait)
    {
        dialogline= newline;
        curline= 0;
        dialogtext.text = dialogline[curline];
        dialogbox.SetActive(true);
        justStart = shouldwait;   
        GameManager.instance.diaact = true; 
    }
}
