using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class zombieControls : MonoBehaviour
{
    public enum Zombie_State { Idle, Chasing, attack, dead}
    public Zombie_State is_currently = Zombie_State.Idle;
    Animator zomb_animation;
    int health;
    int agro_range = 5;
    float speed = 400f;
    private NavMeshAgent agent;
    private GameObject[] players;
    GameObject closestPlayer;
    float dist;
    float attack_range = 1.5f;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        zomb_animation = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        players = GameObject.FindGameObjectsWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

        switch (is_currently)
        {
            case Zombie_State.Idle:

                
               

                foreach (GameObject player in players)
                {

                    dist = Vector3.Distance(this.transform.position, player.transform.position);

                    if (dist <= agro_range)
                    {
                        zomb_animation.SetBool("Activated", true);
                        closestPlayer = player;
                        agent.SetDestination(closestPlayer.transform.position);
                        is_currently = Zombie_State.Chasing;

                    }
                   
                }


                break;

            case Zombie_State.Chasing:

               
                agent.speed = speed * Time.deltaTime;

                dist = Vector3.Distance(this.transform.position, closestPlayer.transform.position);
                agent.SetDestination(closestPlayer.transform.position);

                if (dist <= attack_range)
                {
                    agent.SetDestination(closestPlayer.transform.position);
                    
                    is_currently = Zombie_State.attack;
                }
                else if (dist > agro_range)//move out of agro range
                {

                    is_currently = Zombie_State.Idle;
                    zomb_animation.SetBool("Activated", false);
                }


                break;


            case Zombie_State.attack:
                

                dist = Vector3.Distance(this.transform.position, closestPlayer.transform.position);
                

                if (dist > attack_range+.25f)
                {   agent.isStopped=false;
                    is_currently = Zombie_State.Chasing;

                    zomb_animation.SetBool("Attack", false);
                    zomb_animation.SetBool("Activated", true);
                }
                else
                {
                    if (dist < 1)
                    {
                        agent.isStopped = true;
                    }

                    zomb_animation.SetBool("Activated", false);
                    
                    zomb_animation.SetBool("Attack", true);
                }


                break;
            case Zombie_State.dead:

                    zomb_animation.SetBool("isDead", true);
                    agent.isStopped = true;
                break;
        }

        

    }
    

}
