using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bacteria : MonoBehaviour
{

    NavMeshAgent agent;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<GameManager>().player;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            target = GetComponent<GameManager>().player;
        }

        agent.SetDestination(target.position);
    }
}
