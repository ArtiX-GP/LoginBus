using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Vector3 targetPosition;
    private Seeker seeker;
    private CharacterController controller;
    //The calculated path
    public Path path;
    //The AI's speed per second
    public float speed = 0.5f;
    //The max distance from the AI to a waypoint for it to continue to the next waypoint
    public float nextWaypointDistance = 1;
    //The waypoint we are currently moving towards
    private int currentWaypoint = 0;
    public void Start () {
        seeker = GetComponent<Seeker>();
        controller = GetComponent<CharacterController>();
        SendToTarget(Vector3.zero);
        //Start a new path to the targetPosition, return the result to the OnPathComplete functio
    }

    public void SendToTarget(Vector3 target)
    {
        seeker.StartPath (transform.position,target, OnPathComplete);
    }
    
    public void OnPathComplete (Path p) {
        Debug.Log ("Yay, we got a path back. Did it have an error? "+p.error);
        if (!p.error) {
            path = p;
            //Reset the waypoint counter
            currentWaypoint = 0;
        }
    }
    public void Update () {
        if (path == null) {
            //We have no path to move after yet
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count) {
            Debug.Log ("End Of Path Reached");
            return;
        }
        //Direction to the next waypoint
        Vector2 dir = (-path.vectorPath[currentWaypoint]+transform.position);
        Debug.Log(path.vectorPath[currentWaypoint]);
        Debug.Log(dir);
        dir *= speed * Time.deltaTime;
        controller.SimpleMove (dir);
        //Check if we are close enough to the next waypoint
        //If we are, proceed to follow the next waypoint
        if (Vector2.Distance (transform.position,path.vectorPath[currentWaypoint]) < nextWaypointDistance) {
            currentWaypoint++;
            return;
        }
    }
}
