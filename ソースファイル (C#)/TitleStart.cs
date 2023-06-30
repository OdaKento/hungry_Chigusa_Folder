using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TitleStart : MonoBehaviour
{
    AudioSource audio;
    [SerializeField]AudioClip clip;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // É^ÉbÉ`Ç≥ÇÍÇΩéûÇ…åƒÇŒÇÍÇÈä÷êî
    public void Main()
    {
        audio.PlayOneShot(clip);
        animator.SetBool("mainClick", true);
    }
    private void mainChangeScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
