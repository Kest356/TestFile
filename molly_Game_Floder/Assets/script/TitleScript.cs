using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TitleScript : MonoBehaviour {

    public GameObject GameRule_group;

    AudioSource audioSource;
    public AudioClip T_Sound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = T_Sound;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void game_Start()
    {
        audioSource.Stop();
        SceneManager.LoadScene("두더지잡기");
    }
    public void Open_rules()
    {
        GameRule_group.gameObject.SetActive(true);
    }
    public void Close_rules()
    {
        GameRule_group.gameObject.SetActive(false);
    }
}
