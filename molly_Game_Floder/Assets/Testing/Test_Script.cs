using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_Script : MonoBehaviour {

    Image myImage;
    public float WT;

	void Start () {
        myImage = GetComponent <Image> ();
        StartCoroutine(FlashImg(WT));
	}
	
    IEnumerator FlashImg(float WaitTime)
    {
        while (true)
        {
            myImage.color =
                new Color(
                    myImage.color.r,
                    myImage.color.g,
                    myImage.color.b,
                    1
            );
            yield return new WaitForSeconds(WaitTime);

            myImage.color =
                new Color(
                    myImage.color.r,
                    myImage.color.g,
                    myImage.color.b,
                    0
            );
            yield return new WaitForSeconds(WaitTime);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopAllCoroutines();
        }
    }
}
