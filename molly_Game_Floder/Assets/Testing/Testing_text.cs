using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Testing_text : MonoBehaviour {

    Text myT;
    public Color C1, C2;
    public float fadingT;

	// Use this for initialization
	void Start () {
        myT = GetComponent<Text>();
        StartCoroutine(chagC(fadingT, C1, C2));
	}
	
	IEnumerator chagC(float fadeT, Color C1, Color C2)
    {
        float t = 0;
        while (t < fadeT)
        {
            t += Time.deltaTime;

            float blend = Mathf.Clamp(t / fadeT, 0, 1f);

            myT.color = Color.Lerp(C1, C2, blend);

            yield return null;
        }
    }
}
