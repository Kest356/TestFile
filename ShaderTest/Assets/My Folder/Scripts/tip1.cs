using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tip1 : MonoBehaviour {

    public Text tipText;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            tipText.text = "you can Jump - Space bar";
        }
    }

}
