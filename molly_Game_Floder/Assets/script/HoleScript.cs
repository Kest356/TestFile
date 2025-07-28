using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoleState
{
    None,
    Open,
    Idle,
    Close,
    Catch
}

public class HoleScript : MonoBehaviour
{
    //두더지들의 상태.
    public MoleState MS;

    //화면 흔들기
    public float duration = 1.0f;
    public float magnitude = 1.0f;
    Transform myCam;

    //이미지들의 묶음.
    public Sprite[] Open_Images;
    public Sprite[] Idle_Images;
    public Sprite[] Close_Images;
    public Sprite[] Catch_Images;

    //어떤 두더지인지 체크하기 위한 값.
    public bool GoodMole;
    public int PerGood;

    public int good;
    public int bad;

    //2nd Mole: 이미지들의 묶음.
    public Sprite[] Open_Images_2;
    public Sprite[] Idle_Images_2;
    public Sprite[] Close_Images_2;
    public Sprite[] Catch_Images_2;

    //애니메이션 속도를 컨트롤하기 위한 변수.
    public float Ani_Speed;
    public float _now_ani_time;

    SpriteRenderer spr;

    int Ani_Count;

    //사운드 플레이용. 
    AudioSource audioSource;

    public AudioClip Open_Sound;
    public AudioClip Catch_Sound;

    public GameManager GM;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        

        myCam = Camera.main.transform;
    }

    void Update()
    {
        if (_now_ani_time >= Ani_Speed)
        {
            if (MS == MoleState.Open)
            {
                Open_Ing();
            }

            if (MS == MoleState.Idle)
            {
                Idle_Ing();
            }

            if (MS == MoleState.Close)
            {
                Close_Ing();
            }

            if (MS == MoleState.Catch)
            {
                Catch_Ing();
            }

            _now_ani_time = 0;
        }
        else
        {
            _now_ani_time += Time.deltaTime;
        }

    }

    public void Open_On()
    {
        MS = MoleState.Open;
        Ani_Count = 0;
        audioSource.clip = Open_Sound;
        audioSource.Play();

        int a = Random.Range(0, 100);

        if (a > PerGood)
        {
            GoodMole = true;
        }
        else
        {
            GoodMole = false;
        }
    }

    public void Idle_On()
    {
        MS = MoleState.Idle;
        Ani_Count = 0;
    }

    public void Close_On()
    {
        MS = MoleState.Close;
        Ani_Count = 0;
    }

    public void Catch_On()
    {
        MS = MoleState.Catch;
        Ani_Count = 0;
        audioSource.clip = Catch_Sound;
        audioSource.Play();

        if (GoodMole == true)
            GM.Count_Good += 1;
        else
            GM.Count_Bad += 1;

    }

    public void Catch_Ing()
    {
        if (GoodMole == true)
        {
            spr.sprite = Catch_Images[Ani_Count];
        }
        else
        {
            spr.sprite = Catch_Images_2[Ani_Count];
        }

        Ani_Count += 1;

        if (Ani_Count >= Catch_Images.Length)
        {
            StartCoroutine("Wait");
        }
    }

    public void Open_Ing()
    {

        if (GoodMole == true)
        {
            spr.sprite = Open_Images[Ani_Count];
        }
        else
        {
            spr.sprite = Open_Images_2[Ani_Count];
        }

        Ani_Count += 1;

        if (Ani_Count >= Open_Images.Length)
        {
            Close_On();
        }
    }

    public void Idle_Ing()
    {

        if (GoodMole == true)
        {
            spr.sprite = Idle_Images[Ani_Count];
        }
        else
        {
            spr.sprite = Idle_Images_2[Ani_Count];
        }

        Ani_Count += 1;

        if (Ani_Count >= Idle_Images.Length)
        {
            Close_On();
        }
    }

    public void Close_Ing()
    {
        if (GoodMole == true)
        {
            spr.sprite = Close_Images[Ani_Count];
        }

        else
        {
            spr.sprite = Close_Images_2[Ani_Count];
        }

        Ani_Count += 1;

        if (Ani_Count >= Close_Images.Length)
        {
            StartCoroutine("Wait");
        }
    }

    public IEnumerator Wait()
    {
        MS = MoleState.None;
        Ani_Count = 0;
        float wait_Time = Random.Range(0.5f, 4.5f);
        yield return new WaitForSeconds(wait_Time);

        Open_On();
    }

    public void OnMouseDown()
    {
        if (MS == MoleState.Idle || MS == MoleState.Open || MS == MoleState.Close)
        {
            Catch_On();
            StartCoroutine("startBigScreenShake");
            callParticle();
        }
    }

    void callParticle()
    {
        int r = Random.Range(0, 3); ;

        if( r == 0)
        {
            GameObject ClickParticle = Instantiate(Resources.Load("clickParticle1"),
            transform.position,
            transform.rotation) as GameObject;

            Destroy(ClickParticle, 1f);
        }
        else if( r == 1)
        {
            GameObject ClickParticle = Instantiate(Resources.Load("clickParticle2"),
            transform.position,
            transform.rotation) as GameObject;

            Destroy(ClickParticle, 1f);
        }
        else
        {
            GameObject ClickParticle = Instantiate(Resources.Load("clickParticle3"),
            transform.position,
            transform.rotation) as GameObject;

            Destroy(ClickParticle, 1f);
        }

            
    }

    IEnumerator shakeSequence()     //화면 흔들기
    {
        float elapsed = 0.0f;

        Vector3 originalCamPos = myCam.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            float x = Random.value * 2.0f - 1.0f;
            float z = Random.value * 2.0f - 1.0f;
            x *= magnitude * damper;
            z *= magnitude * damper;
            
            x += originalCamPos.x;
            z += originalCamPos.z;

            Camera.main.transform.position = new Vector3(x, originalCamPos.y, z);

            yield return null;
        }
        myCam.position = originalCamPos;
    }
    void startBigScreenShake()
    {
        duration = 0.2f;
        magnitude = 0.2f;
        startScreenShake();
    }
    public void startScreenShake()
    {
        StartCoroutine("shakeSequence");
    }
}

