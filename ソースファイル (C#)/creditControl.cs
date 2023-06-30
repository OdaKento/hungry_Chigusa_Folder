using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creditControl : MonoBehaviour
{
        
    [SerializeField, Header("�J�����I�u�W�F�N�g")] GameObject _camera;               // �J�����̃I�u�W�F�N�g���Ǘ�����ϐ�
    [SerializeField, Header("�J�����̈ړ��ʒu")] GameObject [] _cameraPosition;      // �J�����̈ʒu���Ǘ�����ϐ�
    [SerializeField, Header("�{�^���I�u�W�F�N�g")] GameObject [] _buttonObj;         // �{�^���I�u�W�F�N�g���Ǘ�����ϐ�
    [SerializeField, Header("�N���W�b�g�̃e�L�X�g")] Text _creditText;               // �e�L�X�g�I�u�W�F�N�g���Ǘ�����ϐ�
    [SerializeField, Header("�{�^���̕���")] string[] _text;                         // �e�L�X�g�̃I�u�W�F�N�g�̔z����Ǘ�����ϐ�

    AudioSource audio;              // SE�̃R���g���[���[���Ǘ�����ϐ�
    public AudioClip clip;          // ���ʉ����Ǘ�����ϐ�
    Animator animator;              // �A�j���[�V�����̃R���g���[���[���Ǘ�����ϐ�

    public enum TitleType
    {
        title = 0,
        credit,
    };
    [SerializeField,Header("�^�C�g���V�[���̏��")]TitleType _title;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Unity��ŌĂяo�����Ƃ��ł���֐�
    // �N���b�N���ɌĂяo���֐�
    public void _OnClick()
    {
        audio.PlayOneShot(clip);                    // ���ʉ���炷
        animator.SetBool("creditClick", true);      // �A�j���[�V�������Đ�����
        Invoke("_cameraPos", 0.5f);                 // ���ԍ��ŃJ�����̈ʒu��ύX����
    }

    // �Q�[���I�u�W�F�N�g���\���ɂ��郁�\�b�h
    private void _setActive(int _loopCount , bool _bActive)
    {
        // �z��̐������[�v����
        for(int i =0;i<_loopCount;i++)
        {
            // �Q�[���I�u�W�F�N�g���\���ɂ���
            _buttonObj[i].SetActive(_bActive);
        }
    }

    // �J�����̈ʒu���X�V���郁�\�b�h
    private void _cameraPos()
    {
        // �^�C�g���̃V�[������N���W�b�g�֕ύX���鎞�̏���
        if (_title == TitleType.title)
        {
            _title = TitleType.credit;
            _setActive(_buttonObj.Length, false);               // �I�u�W�F�N�g���\���ɂ���
            _creditText.text = _text[(int)_title].ToString();   // ������ύX����
        }
        // �N���W�b�g�̃V�[������^�C�g���֕ύX���鎞�̏���
        else if (_title == TitleType.credit)
        {
            _title = TitleType.title;
            _setActive(_buttonObj.Length, true);                // �I�u�W�F�N�g���\���ɂ���
            _creditText.text = _text[(int)_title].ToString();   // ������ύX����
        }

        // �J�����̈ʒu���X�V����
        _camera.transform.position = _cameraPosition[(int)_title].transform.position;
        
        // �A�j���[�V�����𓮂���
        animator.SetBool("creditClick", false);
    }
}
