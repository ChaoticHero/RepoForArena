using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
         
        if (other.name == "Player")
        {
            agent.destination = player.position;
            Debug.Log("Player detected - attack!");
        }
    }

    
    void OnTriggerExit(Collider other)
    {
        
        if (other.name == "Player")
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }
    private int locationIndex = 0;

    private UnityEngine.AI.NavMeshAgent agent;

    public Transform patrolRoute;
    public Transform player;
    public List<Transform> locations;

    private int _lives = 3;
    public int EnemyLives
    {
        // 2
        get { return _lives; }

        // 3
        private set
        {
            _lives = value;

            // 4
            if (_lives <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy down.");
            }
        }
    }
    void Start()
    {
        InitializePatrolRoute();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        MoveToNextPatrolLocation();
        player = GameObject.Find("Player").transform;
    }

    
    void InitializePatrolRoute()
    {
        
        foreach (Transform child in patrolRoute)
        {
           
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }
    void Update()
    {
        
        if (agent.remainingDistance < 0.2f && !agent.pathPending)
        {
            
            MoveToNextPatrolLocation();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        // 5
        if (collision.gameObject.name == "Bullet(Clone)")
        {
            // 6
            EnemyLives -= 1;
            Debug.Log("Critical hit!");
        }
    }
}
