using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EFTMg : MonoBehaviour {

    public GameObject eftPar;
	public float eftTime = 0.2f;

	void Start () {
		
	}

	void Update () {

		eftTime -= Time.deltaTime;

        if(eftTime <= 0){
             Destroy(eftPar);
        }
	}
}
