using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{

    AudioSource audioSource;
    [SerializeField] AudioClip audioClip;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Title()
    {
        audioSource.PlayOneShot(audioClip);            // 決定ボタンの効果音を鳴らす 
        animator.SetBool("titleClick", true);          // クリック時のアニメーションを再生する
    }
    private void titleChangeScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

}
