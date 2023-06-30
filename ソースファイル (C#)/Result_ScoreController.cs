using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result_ScoreController : MonoBehaviour
{
    [SerializeField] Text scoreGauge;
    [SerializeField] int prevScore;
    int saveScore;
    int maxScore = 3000;
    int score = 0;

    private Animator animator;

    private AudioSource audiosource;
    [SerializeField] AudioClip audioclip;
    bool audioPlayflag;

    // Start is called before the first frame update
    void Start()
    {
        saveScore = GameRule.resultScore;
        Debug.Log(saveScore);
        animator = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        //score = saveScore;
    }

    // Update is called once per frame
    void Update()
    {
        Gauge();
    }

    void Gauge()
    {
        // �X�R�A�̑����͈̔͂𐧌�����
        score = Mathf.Clamp(score, 0, saveScore);
        
        //�X�R�A�����Z����
        if(score < saveScore)
        {
            score += 15;
        }
        else
        {
            // �摜�̃A�j���[�V������ ON �ɂ���
            animator.SetBool("startAnimation",true);
        }

        //�X�R�A�̃e�L�X�g��\��
        scoreGauge.text = score.ToString();
    }

    void PlayAudio()
    {
            if(!audioPlayflag)
            {
                audiosource.PlayOneShot(audioclip);         // �u�L���b�v�̌��ʉ�����x�����炷
                audioPlayflag = true;                       // ���ʉ��̍Đ��t���O�� ON �ɂ���
            }
    }
}
