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
        audioSource.PlayOneShot(audioClip);            // ����{�^���̌��ʉ���炷 
        animator.SetBool("titleClick", true);          // �N���b�N���̃A�j���[�V�������Đ�����
    }
    private void titleChangeScene()
    {
        SceneManager.LoadScene("TitleScene");
    }

}
