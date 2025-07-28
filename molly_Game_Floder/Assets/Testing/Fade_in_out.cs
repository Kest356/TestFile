using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Fade_in_out : MonoBehaviour {

    SpriteRenderer mySp;
    public float sp;

    bool fadeIn = true;

	// Use this for initialization
	void Start () {
        mySp = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeInOut(fadeIn,sp));

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FadeInOut(bool fadeIn, float fadeTime)
    {
        float t = 0;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            float blend = Mathf.Clamp(t/fadeTime,0,1f);

            if (fadeIn)
            {
                mySp.color = new Color(
                    mySp.color.r,
                    mySp.color.g,
                    mySp.color.b,
                    Mathf.Lerp(0, 1f, blend)
                    );
            }

            else
            {
                mySp.color = new Color(
                   mySp.color.r,
                   mySp.color.g,
                   mySp.color.b,
                   Mathf.Lerp(1f, 0, blend)
                   );
            }

            yield return null;
        }
    }
}
