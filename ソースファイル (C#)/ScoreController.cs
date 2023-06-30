using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    enum Alphabet
    {
        E,D,C,B,A,S
    };
    Alphabet rank_alphabet;
    [SerializeField] int [] maxScore = new int [6];
    int red; int green; int blue;           // RGB の値を管理する
    int maxColor = 255;                     // RGB の最大値を設定する変数

    public static int Point;

    int score = 0;
    float point = 0;
    float Barpoint = 0;
    float percentTime = 0.0f;

    [SerializeField] Image scoreBar;        // スコアバーを設定する
    [SerializeField] int maxPoint = 1000;   // 最大スコア
    public static int maxResultPoint; 

    [SerializeField] Image percentGauge;    // ゲージを管理する
    [SerializeField] int maxgaugeTime;      // ゲージの最大時間
    [SerializeField] Text percentText;
    float prevPercent;

    //スコアの上昇する割合を管理する
    float percent;
    [SerializeField] float maxUpPercent;
    //スコアが上昇するまでの基準数を管理する
    float criterion;
    [SerializeField] float defalutCriterion;
    //基準が上昇する割合を管理する
    [SerializeField] float increase = 1.0f;

    ItemController.Foods foods;

    public void AddScore()
    {
        score++;
        percentTime = maxgaugeTime;
    }
    public void setScore()
    {
        score = 0;
        criterion = defalutCriterion;
        percent = 1.0f;
        setEnable(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        maxScore[5] = maxPoint;
        maxResultPoint = maxPoint;

        point = 0;
        criterion = defalutCriterion;
        percent = 1.0f;
        percentTime = maxgaugeTime;
        setEnable(false);
    }

    // Update is called once per frame
    void Update()
    {
        Point = (int)point;
        changeRank(Point);
        UpPercent();
        ScoreBar();
        percentTimer();
        percentText.text = prevPercent.ToString();
    }

    // ScoreBarの範囲を設定する
    void ScoreBar()
    {
        if(point < 0) { point = 0; }

        Barpoint = point;
        Barpoint = Mathf.Clamp(Barpoint, 0, maxPoint);
        scoreBar.fillAmount = (float)point /  (float)maxPoint;
    }

    //
    void changeRank(int _point)
    {
        // スコアの値ごとに評価ランクを変更する
        if(_point < maxScore[0])
        { rank_alphabet = Alphabet.E; }
        else if(_point <  maxScore[1])
        { rank_alphabet = Alphabet.D; }
        else if(_point <  maxScore[2])
        { rank_alphabet = Alphabet.C; }
        else if(_point <  maxScore[3])
        { rank_alphabet = Alphabet.B; }
        else if(_point <  maxScore[4])
        { rank_alphabet = Alphabet.A; }
        else if(_point <  maxScore[5])
        { rank_alphabet = Alphabet.S; }

        ScoreColor();
    }

    // ScoreBarの色を変更する
    void ScoreColor()
    {
        // スコアゲージの色を変更する
        switch (rank_alphabet)
        {
            case Alphabet.E:
                red = 185; green = 200; blue = 255;
                break;
            case Alphabet.D:
                red = 185 ; green = 255; blue = 255;
                break;
            case Alphabet.C:
                red = 200; green = 255; blue = 200;
                break;
            case Alphabet.B:
                red = 255; green = 255; blue = 185;
                break;
            case Alphabet.A:
                red = 255; green = 255; blue = 185;
                break;
            case Alphabet.S:
                red = 255; green = 185; blue = 200;
                break;
        }
        Debug.Log("ランク：" + rank_alphabet);
        scoreBar.color = new Color(red /maxColor, green / maxColor, blue / maxColor);
    }

    // point を加算する
    public void AddPoint(int _point)
    {
        point += _point * percent;
    }
    


    // スコアの上昇率を設定する
    void UpPercent()
    {
        //
        if(score > (int)criterion)
        {
            
            setEnable(true);                    // スコア上昇値のゲージを表示させる
            criterion = criterion * increase;   // 
            percent = percent * 1.1f;           // 上昇率を 1.1倍する
            
            // 上昇率の上限に達したら値を固定する
            if (percent > maxUpPercent)
            {
                percent = maxUpPercent;
            }
        }
        prevPercent = percent;
    }

    // percentObject のカウントダウンを変更する
    void percentTimer()
    {
        percentTime = Mathf.Clamp(percentTime, 0.0f, maxgaugeTime);
        percentTime -= Time.deltaTime;  
        percentGauge.fillAmount = percentTime / (float)maxgaugeTime;
        
        if(percentTime < 0.0f)
        {
            setScore();
        }
    }

    // percentObjects の表示を変更する
    void setEnable(bool _ebabled)
    {
        //Debug.Log("オブジェクトのactive" + _ebabled);
        percentGauge.enabled = _ebabled;
        percentText.enabled = _ebabled;
    }


}
