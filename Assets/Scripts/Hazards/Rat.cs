using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rat : MonoBehaviour
{
    public GameObject spawnOrigin;
    public float ratHealth;

    public NavMeshAgent navAgent;
    public float wanderRadius;
    public float wanderTimer;

    private Transform target;
    private float timer;
    private bool hunting;

    // Start is called before the first frame update
    void Awake ()
    {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= wanderTimer && !hunting)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            navAgent.SetDestination(newPos);
            timer = 0;
        }
        
        if(ratHealth <= 0)
        {
            if (spawnOrigin != null)
                spawnOrigin.GetComponent<Mess>().currentRats -= 1f;
            Destroy(gameObject);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int LayerMask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, LayerMask);
        return navHit.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            navAgent.destination = other.transform.position;
            hunting = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Player"))
        {
            navAgent.destination = other.transform.position;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == ("Player"))
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            navAgent.SetDestination(newPos);
            timer = 0;
            hunting = false;
        }
    }
}
