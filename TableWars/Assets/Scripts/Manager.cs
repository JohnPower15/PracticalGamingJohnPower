using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Manager : MonoBehaviour
{
    List<Player_Movement> players;
    Player_Movement currentlySelectedPlayer;
    public GameObject zombieTemplate;
    List<GameObject> spawnPoints;
    List<zombieControls> zombies;
    private float MaxRange = 5f;
    GameObject curent_zombie;
    Vector3 zombiePos;
    Vector3 players_target_pos;
    public static bool movingBackwards;


    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<Player_Movement>().ToList();

        currentlySelectedPlayer = players[0];

        zombies = new List<zombieControls>();
        spawnPoints = GameObject.FindGameObjectsWithTag("Respawn").ToList();
    }

    // Update is called once per frame
    void Update()
    {
        proximatyWarning();
        moving_away();
        if (Input.GetMouseButtonDown(0))
        {


            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit))
            {
                currentlySelectedPlayer.newDestinationIs(hit.point);
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            currentlySelectedPlayer = getNextPlayer();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentlySelectedPlayer.executeAction();
         }
       
        if (Input.GetKeyDown(KeyCode.Z))
        {
            spawnZombie();
        }
    }

    private void proximatyWarning()
    {
        foreach (Player_Movement player in players)
        {
            foreach(zombieControls zombie in zombies)
            {
                if (Vector3.Distance(player.transform.position, zombie.transform.position) < MaxRange)
                {
                    player.proximatyWarning(zombie);
                    zombiePos = player.findNearestZombie(zombie);
                }
            }
        }
    }

    private Player_Movement getNextPlayer()
    {
        int index = players.IndexOf(currentlySelectedPlayer);
        index++;
        index = index % players.Count();

        return players[index];
    }

    private void spawnZombie()
    {
        int i = UnityEngine.Random.Range(0, spawnPoints.Count());


        GameObject zombieGO = Instantiate(zombieTemplate, spawnPoints[i].transform.position,Quaternion.identity);
        zombies.Add(zombieGO.GetComponent<zombieControls>());

    }    

    public void moving_away()
    {
        Vector3 target =currentlySelectedPlayer.final_destination_actual;
        Vector3 currentPlayer = currentlySelectedPlayer.transform.position;
        Vector3 zombie_player =zombiePos;
        
        float cp_to_target = Vector3.Distance(currentPlayer, target);
        float zp_to_target = Vector3.Distance(zombie_player, target);

        if (cp_to_target<zp_to_target)
        {
            //currentlySelectedPlayer.char_animation.SetBool("movingAway", true);
            movingBackwards = true;
            print("yyy");
        }
        else
        {
            //currentlySelectedPlayer.char_animation.SetBool("movingAway", false);
            movingBackwards = false;
            print("xxx");
        }

    }

}


