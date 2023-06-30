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
        // スコアの増加の範囲を制限する
        score = Mathf.Clamp(score, 0, saveScore);
        
        //スコアを加算する
        if(score < saveScore)
        {
            score += 15;
        }
        else
        {
            // 画像のアニメーションを ON にする
            animator.SetBool("startAnimation",true);
        }

        //スコアのテキストを表示
        scoreGauge.text = score.ToString();
    }

    void PlayAudio()
    {
            if(!audioPlayflag)
            {
                audiosource.PlayOneShot(audioclip);         // 「キラッ」の効果音を一度だけ鳴らす
                audioPlayflag = true;                       // 効果音の再生フラグを ON にする
            }
    }
}
