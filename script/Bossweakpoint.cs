using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossweakpoint : MonoBehaviour
{
    public BossController theBC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void damgeBoss(int damage)
    {
        theBC.damge(damage);
    }
}
