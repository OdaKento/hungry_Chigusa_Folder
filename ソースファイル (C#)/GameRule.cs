using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameRule : MonoBehaviour
{
    [SerializeField]Text textTimer;
    [SerializeField] float deftimer;
    float timer;
    int prevTimer;
    public static bool ActionFlag;
    [SerializeField] Text textFinish;

    [SerializeField]AudioClip finishSound;
    AudioSource m_finishAudioSource;
    bool playAudio = false;


    [SerializeField] float changeTime = 1.0f;
    int savePoint;
    public static int resultScore;

    public static bool menuFlag = false;        //メニュー画面かを管理するフラグ

    // Start is called before the first frame update
    void Start()
    {
        textFinish.fontSize = 200;
        m_finishAudioSource = GetComponent<AudioSource>();
        ActionFlag = true;
        timer = deftimer;
        textFinish.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //キーボードMを押すとメニュー画面に切り替える
        if (Input.GetKeyDown(KeyCode.M))
        {
            menuFlag = !menuFlag;
            pauseMenu();
        }

        Timer();
        textTimer.text = prevTimer.ToString();
    }

    void Timer()
    {
        timer = Mathf.Clamp(timer, 0.0f, deftimer);
        timer -= Time.deltaTime;
        if(timer <= 0.0f )
        {
            savePoint = ScoreController.Point;
            textFinish.enabled = true;
            text(playAudio);
            if (!playAudio)
            {
                saveScore();
                Invoke("ChangeScene", changeTime);
                Debug.Log("スコア：" + resultScore);
                m_finishAudioSource.PlayOneShot(finishSound);
                playAudio = true;
            }
            ActionFlag = false;
        }
        prevTimer = (int)timer;
    }

    void text(bool _textFlag)
    {
        if(_textFlag)
        {
            textFinish.fontSize--;
            textFinish.fontSize = Mathf.Clamp(textFinish.fontSize,100,200);
        }
    }

    void saveScore()
    {
        resultScore = savePoint;
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("ResultScene");
    }

    void pauseMenu()
    {
        if (menuFlag && ActionFlag)
        {
            Debug.Log("一時停止");
            Time.timeScale = 0.0f;
        }
        else
        {
            Debug.Log("ゲームを再開");
            Time.timeScale = 1.0f;
        }
    }
}
