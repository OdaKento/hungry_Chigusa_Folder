using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : GameRule
{
    public Vector2 startPos;
    public Vector2 endPos;
    public Vector2 CenterUp;

    bool turn = false;
    [SerializeField] int maxCnt;

    GameObject scoreObj;
    int cnt;

    public enum Foods
    {
        favorite,
        normal,
        disliked
    };
    public Foods p_FoodType;
    [SerializeField] GameObject itemIconObject;     // �A�C�R���̃I�u�W�F�N�g���Ǘ�����ϐ�         
    [SerializeField] bool ActiveIcon;               // �A�C�R���̕\���E��\�����Ǘ�����ϐ� ( true�F�\��  false�F��\�� ) 

    [SerializeField] int point;
    [SerializeField] int red;
    [SerializeField] int green;
    [SerializeField] int blue;
    int colorMax = 255;

    [SerializeField,Header("�l�����̃{�C�X")] AudioClip _sound;
    [SerializeField, Header("�`�O�T�̊�")] Sprite _sprite;
    PlayerController player;

    SpriteRenderer m_SR;
    [SerializeField] SpriteRenderer[] m_Sprite;

    // Start is called before the first frame update
    void Start()
    {
        scoreObj = GameObject.Find("ScorePoint");
        player = GameObject.Find("Capsule").GetComponent<PlayerController>();

        m_SR = GetComponent<SpriteRenderer>();

        setActiveIcon();

        m_SR.sprite = m_Sprite[RandomSprite(m_Sprite.Length)].GetComponent<SpriteRenderer>().sprite;
        m_SR.color = new Color(red / colorMax, green / colorMax, blue / colorMax);      // �X�R�A�Q�[�W�̐F��ݒ肷��
    }

    // Update is called once per frame
    void Update()
    {

        if (!turn)
        {
            Vector2 tmp_AC = Vector2.Lerp(startPos, CenterUp, (float)cnt / (float)maxCnt);
            Vector2 tmp_CB = Vector2.Lerp(CenterUp, endPos, (float)cnt / (float)maxCnt);
            Vector2 tmp_AB = Vector2.Lerp(tmp_AC, tmp_CB, (float)cnt / (float)maxCnt);
            transform.position = tmp_AB;
            cnt++;
            if (cnt >= maxCnt)
            {
                cnt = 0;
                turn = true;
            }
        }
    }

    int RandomSprite(int _num)
    {

        int iElement = Random.Range(0, _num);
        return iElement;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.setAudioType(_sound,_sprite);
            if (p_FoodType != Foods.disliked)
            {
                scoreObj.GetComponent<ScoreController>().AddScore();
                scoreObj.GetComponent<ScoreController>().AddPoint(point);
            }
            else
            {
                scoreObj.GetComponent<ScoreController>().setScore();
                scoreObj.GetComponent<ScoreController>().AddPoint(point);
            }
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "DestroyLine")
        {
            Destroy(this.gameObject);
        }
    }

    void setActiveIcon()
    {
        itemIconObject.SetActive(ActiveIcon);
    }

}
