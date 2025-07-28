using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public enum GameState
{
    Ready,
    Play,
    End
}

public class GameManager : MonoBehaviour
{
    public GameState GS;                       //게임매니져의 상태관리.

    public HoleScript[] Holes;                 //구멍 스크립트들(배열).
    public float LimitTime;                    //게임 제한시간.
    public Text TimeText;                      //게임 제한시간을 표기하기 위한GUIText.

    public int Count_Bad;                      //나쁜두더지를 잡은 횟수.
    public int Count_Good;                     //착한 두더지를 잡은 횟수.

    int G_num = 10;                            // +100점 두더지
    float B_num = 1;                           // -1000점 두더지

    public GameObject FinishGame_Group;        //결과화면을 보여주기 위한 오브젝트.
    public Text Final_Count_Bad;               //결과화면에서 나쁜 두더지를 잡은 숫자를 보여 줄 GUIText.
    public Text Final_Count_Good;              //결과화면에서 착한 두더지를 잡은 숫자를 보여 줄 GUIText.
    public Text Final_Score;                   //결과화면에서 착한 두더지를 잡은 숫자를 보여 줄 GUIText.

    public Button AddButton;

    public Image endBG, startBG;

    AudioSource audio_Source;

    public AudioClip Go_Sound;
    public AudioClip Ready_Sound;
    public AudioClip Win_Sound;

    void Start()
    {
        GS = GameState.Ready;

        audio_Source = GetComponent<AudioSource>();

        StartCoroutine(game_flash(4,1.5f,1f));
    }

    void Update()
    {
        if (GS == GameState.Play)
        {
            LimitTime -= Time.deltaTime;

            if (LimitTime <= 0)
            {
                LimitTime = 0;
                End();
            }
        }
        TimeText.text = string.Format("{0:N2}", LimitTime);
        ShowAddT();
    }

    public void GO()
    {
        GS = GameState.Play;
        audio_Source.clip = Go_Sound;
        audio_Source.Play();
    }

    void End()
    {
        GS = GameState.End;
        Final_Count_Bad.text = Count_Bad.ToString();
        Final_Count_Good.text = Count_Good.ToString();
        Final_Score.text = (Count_Good * 100 - Count_Bad * 1000).ToString();

        FinishGame_Group.gameObject.SetActive(true);
        StartCoroutine(fadeIn_Menu(1f));

        audio_Source.clip = Win_Sound;
        audio_Source.Play();
    }

     public void ReStart()
     {
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
     }

    IEnumerator fadeIn_Menu(float fadeTime)
    {
        CanvasGroup menuCanvas = FinishGame_Group.GetComponent<CanvasGroup>();

        float t = 0;

        while (t < fadeTime)
        {
            t += Time.deltaTime;
            
            float blend = Mathf.Clamp(t / fadeTime, 0, 1f);
            menuCanvas.alpha = Mathf.Lerp(0, 1f, blend);

            yield return null;
        }
    }

    IEnumerator game_flash(int many, float time, float fade)
    {
        Text startT = GameObject.FindWithTag("Game_Start").GetComponent<Text>();
        float flash_time = time / many;
        int i = 0;

        while (i < many)
        {
            startT.color = new Color(startT.color.r,
                                     startT.color.g,
                                     startT.color.b,
                                     1);

            yield return new WaitForSeconds(flash_time);

            startT.color = new Color(startT.color.r,
                                     startT.color.g,
                                     startT.color.b,
                                     0);
            yield return new WaitForSeconds(flash_time);

            i++;
        }

        float t = 0;

        while (t < fade)
        {
            t += Time.deltaTime;

            float blend = Mathf.Clamp(t / fade, 0, 1f);

            startBG.color = new Color(startBG.color.r,
                                      startBG.color.g,
                                      startBG.color.b,
                                      Mathf.Lerp(1f, 0, blend));

            yield return null;
        }

        for (int u = 0; u < Holes.Length; u++)
        {
            Holes[u].enabled = true;
        }
        GO();
        audio_Source.clip = Ready_Sound;
        audio_Source.Play();        
    }

    public void ShowAddT()
    {    
        if (Count_Good >= G_num && Count_Bad <= B_num)
        {
            AddButton.gameObject.SetActive(true);
            G_num += 6;
            B_num += 0.3f;
        }
        else if(Count_Bad >= B_num)
        {
            deleteTime();
        }
    }
    public void AddTime()
    {
        LimitTime += 4.0f;
        AddButton.gameObject.SetActive(false);
        StartCoroutine(Tcc(0, 0, 255));
    }
    public void deleteTime()
    {      
        LimitTime -= 8.0f;
        B_num += 1;
        G_num += 15;
        StartCoroutine(Tcc(255,0,0));
    }

    IEnumerator Tcc(int r, int g, int b)
    {
        int i = 0;

        while (i < 3)
        {
            TimeText.color = new Color(r, g, b);

            yield return new WaitForSeconds(0.2f);

            TimeText.color = new Color(255, 255, 255);

            yield return new WaitForSeconds(0.2f);

            i++;
        }
    }
}