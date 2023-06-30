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
    int red; int green; int blue;           // RGB �̒l���Ǘ�����
    int maxColor = 255;                     // RGB �̍ő�l��ݒ肷��ϐ�

    public static int Point;

    int score = 0;
    float point = 0;
    float Barpoint = 0;
    float percentTime = 0.0f;

    [SerializeField] Image scoreBar;        // �X�R�A�o�[��ݒ肷��
    [SerializeField] int maxPoint = 1000;   // �ő�X�R�A
    public static int maxResultPoint; 

    [SerializeField] Image percentGauge;    // �Q�[�W���Ǘ�����
    [SerializeField] int maxgaugeTime;      // �Q�[�W�̍ő厞��
    [SerializeField] Text percentText;
    float prevPercent;

    //�X�R�A�̏㏸���銄�����Ǘ�����
    float percent;
    [SerializeField] float maxUpPercent;
    //�X�R�A���㏸����܂ł̊�����Ǘ�����
    float criterion;
    [SerializeField] float defalutCriterion;
    //����㏸���銄�����Ǘ�����
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

    // ScoreBar�͈̔͂�ݒ肷��
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
        // �X�R�A�̒l���Ƃɕ]�������N��ύX����
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

    // ScoreBar�̐F��ύX����
    void ScoreColor()
    {
        // �X�R�A�Q�[�W�̐F��ύX����
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
        Debug.Log("�����N�F" + rank_alphabet);
        scoreBar.color = new Color(red /maxColor, green / maxColor, blue / maxColor);
    }

    // point �����Z����
    public void AddPoint(int _point)
    {
        point += _point * percent;
    }
    


    // �X�R�A�̏㏸����ݒ肷��
    void UpPercent()
    {
        //
        if(score > (int)criterion)
        {
            
            setEnable(true);                    // �X�R�A�㏸�l�̃Q�[�W��\��������
            criterion = criterion * increase;   // 
            percent = percent * 1.1f;           // �㏸���� 1.1�{����
            
            // �㏸���̏���ɒB������l���Œ肷��
            if (percent > maxUpPercent)
            {
                percent = maxUpPercent;
            }
        }
        prevPercent = percent;
    }

    // percentObject �̃J�E���g�_�E����ύX����
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

    // percentObjects �̕\����ύX����
    void setEnable(bool _ebabled)
    {
        //Debug.Log("�I�u�W�F�N�g��active" + _ebabled);
        percentGauge.enabled = _ebabled;
        percentText.enabled = _ebabled;
    }


}
