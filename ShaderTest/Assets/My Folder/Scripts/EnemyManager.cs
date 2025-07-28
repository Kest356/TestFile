using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour {

    private Transform _pos;
    private Transform playerPos;
    private NavMeshAgent nvA;

    public bool nvStart = false;

    void Start () {
        _pos = this.gameObject.GetComponent<Transform>();
        playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvA = this.gameObject.GetComponent<NavMeshAgent>();

    }
	
	void Update () {

        if(nvStart)
             nvA.destination = playerPos.position;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")     
            nvStart = true;
    }
}
