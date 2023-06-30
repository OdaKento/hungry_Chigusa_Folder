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

        Debug.Log("最初の画像："+ _prevSprite);
    }

    // Update is called once per frame
    void Update()
    {
        // 制限時間が 0秒以下 になった際にアイテムの衝突判定を切り替える
        if (ActionFlag)
        { Itemcollider.SetActive(true); }           // アイテムとの衝突判定を ON にする
        else
        { Itemcollider.SetActive(false); }          // アイテムとの衝突判定を OFF にする

        // 音声を再生してない時に画像を元に戻す
        if(!m_AudioSource.isPlaying)
        {
            _spRender.sprite = _prevSprite;         // 画像を元に戻す
        }

        Move();
    }

    // キーボート入力での移動処理のメソッド
    private void Move()
    {
        playerPos = transform.position;

        velocity_X = Input.GetAxis("Horizontal") * speed;
        rb2D.velocity = new Vector2(velocity_X, rb2D.velocity.y);

        this.playerPos.x = Mathf.Clamp(this.playerPos.x, -rangeX, rangeX);          // プレイヤーの移動距離を制限する
        transform.position = new Vector2(playerPos.x, transform.position.y);        // X座標を更新する
    }

    public void setAudioType(AudioClip _audio,Sprite _sprite)
    {
        m_AudioSource.Stop();                // 音声の再生を一時停止する
        Sound = _audio;                      // 音声を入れ替える
        _spRender.sprite = _sprite;          // 画像を入れ替える
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item" && ActionFlag )
        {
            m_AudioSource.PlayOneShot(Sound);
        }
    }
}
