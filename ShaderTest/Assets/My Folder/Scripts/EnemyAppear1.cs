using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAppear1 : MonoBehaviour {

    public EnemyAppear2 EA2;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            EA2.AS = true;
        }
    }
}
