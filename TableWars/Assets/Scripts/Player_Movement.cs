using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

using Random = UnityEngine.Random;

public class Player_Movement : MonoBehaviour
{
    NavMeshPath path;
    Vector3 targetPosition;
    
    bool isMoving = false;
    internal Animator char_animation;
    float maxdistance = 5;

    private NavMeshAgent agent;
    private Camera camera_main;
    private Vector3 last_position;
    private Rigidbody rb;
    private List<Vector3> point;
    private LineRenderer line;
    zombieControls currentTarget;

    internal Vector3 final_destination_actual;


    void Start()
    {
        char_animation = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

       line = GetComponent<LineRenderer>();
       line.material.color = Color.white;

        line.startWidth = 0.1f;
        line.endWidth = 0.1f;

    }


    void Update()
    {
   

        if (agent.velocity.magnitude > 0.005f)
        {
            isMoving = true;
        }
        if (agent.velocity.magnitude < 0.05f)
        {
            isMoving = false;
            
        }


        if (isMoving)
        {
            //Manager manager = new Manager();
            
            if (Manager.movingBackwards)
            {
                char_animation.SetBool("moving_back", true);
            }
            else
            {
                char_animation.SetBool("is_walking", true);
            }
            line.enabled = false;
            
        }
        else
        {
            char_animation.SetBool("is_walking", false);
            char_animation.SetBool("moving_back", false);
        }

        if (currentTarget)
        {
            char_animation.SetBool("isShooting", true);
            transform.LookAt(currentTarget.transform);
        }
        else
        {
            char_animation.SetBool("isShooting", false);
            
        }

    }

    internal void executeAction()
    {
        agent.speed = 3.5f;
    }

    internal void newDestinationIs(Vector3 destination)
    {
            agent.SetDestination(destination);
            StartCoroutine(DisplayLineDestination());
        
        agent.speed = 0;
    }



    IEnumerator DisplayLineDestination()
    {
        yield return new WaitUntil(() => agent.hasPath);

        int i = 1;
        float distance = 0;
        line.positionCount = 1;

        line.SetPosition(0, transform.position);

        line.enabled = true;

        while ((i < agent.path.corners.Length )&&( agent.path.corners.Length>1))
        {
            point = agent.path.corners.ToList();

            bool is_finished = false;

            float lastdistance = distance;
            float segment_distance = Vector3.Distance(point[i - 1], point[i]);


            distance += segment_distance;
            line.positionCount++;



            if (distance > maxdistance)
            {
               float distance_to_max = maxdistance - lastdistance;

               float proportion_of_line_segment = distance_to_max / segment_distance;

               final_destination_actual = Vector3.Lerp(point[i - 1], point[i], proportion_of_line_segment);

               agent.SetDestination(final_destination_actual);

               line.SetPosition(i, final_destination_actual);
                
                i = 999;
             
            }
            else
            {   
                line.SetPosition(i, point[i]);
            }

            i++;
          
        }
    }

    internal void proximatyWarning(zombieControls zombie)
    {
        if (currentTarget)
        {
            if (Vector3.Distance(transform.position, currentTarget.transform.position)> Vector3.Distance(transform.position, zombie.transform.position))
            {
                currentTarget = zombie;
            }

        }
        else
        {
            currentTarget = zombie;
        }
    }

    internal Vector3 findNearestZombie(zombieControls zombie)
    {
        if (currentTarget)
        {
            if (Vector3.Distance(transform.position, currentTarget.transform.position) > Vector3.Distance(transform.position, zombie.transform.position))
            {
                currentTarget = zombie;
            }

        }
        else
        {
            currentTarget = zombie;
        }

        return zombie.transform.position;
    }
}
