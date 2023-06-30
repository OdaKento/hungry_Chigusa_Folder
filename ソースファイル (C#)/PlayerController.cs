using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : GameRule
{
    float velocity_X;
    float moveX;
    //float velocity_Y ;
    [SerializeField] float speed;
    [SerializeField] float rangeX;
    Vector2 playerPos;
    Rigidbody2D rb2D;
    SpriteRenderer _spRender;
    Sprite _prevSprite;         

    [SerializeField] GameObject Itemcollider;
    int iElement;
    
    ThrowItemController throwObj;
    public ItemController.Foods p_foodType;

    AudioClip Sound;
    AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        throwObj = GameObject.Find("ThrowRules").GetComponent<ThrowItemController>();
        rb2D = GetComponent<Rigidbody2D>();
        _spRender = GetComponent<SpriteRenderer>();
        _prevSprite = _spRender.sprite;

        Debug.Log("�ŏ��̉摜�F"+ _prevSprite);
    }

    // Update is called once per frame
    void Update()
    {
        // �������Ԃ� 0�b�ȉ� �ɂȂ����ۂɃA�C�e���̏Փ˔����؂�ւ���
        if (ActionFlag)
        { Itemcollider.SetActive(true); }           // �A�C�e���Ƃ̏Փ˔���� ON �ɂ���
        else
        { Itemcollider.SetActive(false); }          // �A�C�e���Ƃ̏Փ˔���� OFF �ɂ���

        // �������Đ����ĂȂ����ɉ摜�����ɖ߂�
        if(!m_AudioSource.isPlaying)
        {
            _spRender.sprite = _prevSprite;         // �摜�����ɖ߂�
        }

        Move();
    }

    // �L�[�{�[�g���͂ł̈ړ������̃��\�b�h
    private void Move()
    {
        playerPos = transform.position;

        velocity_X = Input.GetAxis("Horizontal") * speed;
        rb2D.velocity = new Vector2(velocity_X, rb2D.velocity.y);

        this.playerPos.x = Mathf.Clamp(this.playerPos.x, -rangeX, rangeX);          // �v���C���[�̈ړ������𐧌�����
        transform.position = new Vector2(playerPos.x, transform.position.y);        // X���W���X�V����
    }

    public void setAudioType(AudioClip _audio,Sprite _sprite)
    {
        m_AudioSource.Stop();                // �����̍Đ����ꎞ��~����
        Sound = _audio;                      // ���������ւ���
        _spRender.sprite = _sprite;          // �摜�����ւ���
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item" && ActionFlag )
        {
            m_AudioSource.PlayOneShot(Sound);
        }
    }
}
