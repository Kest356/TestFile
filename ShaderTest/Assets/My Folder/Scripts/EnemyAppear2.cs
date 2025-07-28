using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAppear2 : MonoBehaviour {

    public float ap;
    public Material mat;
    public bool AS = false;
    public bool AS2 = false;
    public int disap = 0;

    void Start () {
        mat.SetFloat("_APP", ap);
    }
	
	void Update () {
       

        if (AS != AS2)
        {
            StartCoroutine("AppearShader");
            AS2 = true;
        }
    }

    IEnumerator AppearShader()
    {
        int t = 0;

        while (t < 30)
        {
            ap -= 0.01f;
            mat.SetFloat("_APP", ap);
            yield return new WaitForSeconds(0.2f);
            t++;
        }
    }

}
