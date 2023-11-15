using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenMover : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;

    [SerializeField] private float speed = 2f;

    bool target = false;

    private void Update()
    {
        if(target) {
            GetComponent<Animator>().SetBool("seePlayer", true);
        } else {
            GetComponent<Animator>().SetBool("seePlayer", false);
        }

        if(!target){

        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        Vector2 dir = waypoints[currentWaypointIndex].transform.position - transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);

        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        }

    }

    void OnTriggerStay2D(Collider2D c) {
        if(c.gameObject.tag == "Player") {
            Vector2 newPos = new Vector2(c.gameObject.transform.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, newPos, Time.deltaTime * speed * 2);
            Vector2 dir = c.gameObject.transform.position - transform.position;
            GetComponent<Animator>().SetFloat("DirX", dir.x);
            target = true;
        } else {
            target = false;
        }
    }

    void OnTriggerExit2D(Collider2D c) {
        if(c.gameObject.tag == "Player") {
            target = false;
        }
    }
}