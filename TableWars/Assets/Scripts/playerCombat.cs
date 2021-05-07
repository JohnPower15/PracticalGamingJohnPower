using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombat : MonoBehaviour
{
    float dist;
    GameObject[] zomboi;

    // Start is called before the first frame update
    void Start()
    {
        zomboi = GameObject.FindGameObjectsWithTag("Zomboi");
        print(zomboi.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
        

        foreach(GameObject zombee in zomboi)
        {
           dist = Vector3.Distance(this.transform.position, zombee.transform.position);
            if (dist < 5)
        {
                zombieControls zombz = new zombieControls();

                zombz.is_currently = zombieControls.Zombie_State.dead;
        }
        }

        
        
    }
}
