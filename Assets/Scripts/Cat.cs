using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour
{
    public float IdleTime { get; private set; } = 2.0f;         // time to spend in idle state
    public float FollowRange { get; private set; } = 5.0f;      // when player is closer than this, follow
    public float WaitRange { get; private set; } = 1.0f;    // when player is closer than this, wait
    private bool isFollowing = false;

    private int treatCount;
    private int followAmount = 5;

    public GameObject Player { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public List<Vector3> Waypoints { get; private set; }      // waypoints for patrol state
    private int waypointIndex = 0;                              // current waypoint index
    private Vector3 initialCatPos;
    private Vector3 wayPoint1;
    private Vector3 wayPoint2;
    private bool hasJustPatrolled;
    void Start()
    {
        treatCount = 0;
        Agent = GetComponent<NavMeshAgent>();                   // get a reference to the NavMeshAgent
        Player = GameObject.FindGameObjectWithTag("Player");    // get a reference to the Player

        // Create and populate a list of waypoints
        Waypoints = new List<Vector3>();
        initialCatPos = transform.position;
        // set initial waypoint to 2 points away from initial position on the z axis
        wayPoint1 = new Vector3(initialCatPos.x, initialCatPos.y, initialCatPos.z - 2);
        Waypoints.Add(wayPoint1);
        // set second waypoint to 2 points away from initial position on the z axis
        wayPoint2 = new Vector3(initialCatPos.x, initialCatPos.y, initialCatPos.z + 2);
        Waypoints.Add(wayPoint2);
    }

    private void OnDrawGizmos()
    {
        //Draw a sphere to show follow range in Scene
        Gizmos.DrawWireSphere(transform.position, FollowRange);
    }

    public float GetDistanceFromPlayer()
    {
        //Get the distance(in units) from the cat to the player
        return Vector3.Distance(transform.position, Player.transform.position);
    }

    public void DetermineNextWaypoint()
    {
        waypointIndex++;
        if (waypointIndex >= Waypoints.Count)
        {
            waypointIndex = 0;
        }
    }

    public Vector3 GetCurrentWaypoint()
    {
        //return the current waypoint
        return Waypoints[waypointIndex];
    }

    public bool isTimeToLookAround()
    {
        return hasJustPatrolled;
    }

    public void SetHasJustPatrolled(bool patrolStatus)
    {
        hasJustPatrolled = patrolStatus;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReactToPlayer()
    {
        if (!isFollowing)
        {
            // if enough treats, follow, otherwise hiss at player
            if (treatCount >= followAmount)
            {
                Messenger.Broadcast(GameEvent.CAT_COLLECTED);
                Debug.Log("cat collected!");
                isFollowing = true;
                // let interested parties know 5 treats were consumed
                Messenger<int>.Broadcast(GameEvent.TREATS_USED, (followAmount * -1));
            }
            else
            {
                Debug.Log("hiss!");
            }
        }
    }
    private void Awake()
    {
        Messenger<int>.AddListener(GameEvent.TREAT_COLLECTED, OnTreatsChanged);
        Messenger<int>.AddListener(GameEvent.TREATS_USED, OnTreatsChanged);
    }

    private void OnDestroy()
    {
        Messenger<int>.RemoveListener(GameEvent.TREAT_COLLECTED, OnTreatsChanged);
        Messenger<int>.RemoveListener(GameEvent.TREATS_USED, OnTreatsChanged);
    }

    private void OnTreatsChanged(int newTreats)
    {
        UpdateTreatsCollected(newTreats);
    }

    // update treats collected display
    public void UpdateTreatsCollected(int newTreatAmount)
    {
        treatCount += newTreatAmount;
    }

    public int GetTreats()
    {
        return treatCount;
    }
}
