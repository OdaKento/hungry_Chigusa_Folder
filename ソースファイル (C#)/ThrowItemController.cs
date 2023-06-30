using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItemController : GameRule
{
    [SerializeField] GameObject prefabFood;
    [SerializeField] GameObject[] ThrowPoint = new GameObject[5];
    [SerializeField] GameObject[] ThrowCharaPoint = new GameObject[2];
    [SerializeField] GameObject[] FoodObject = new GameObject[3];

    [SerializeField] float centerUp = 1.0f;

    Vector2 foodPosition;
    bool throwF;
    [SerializeField]int iRand;
    int prevRand;

    [SerializeField]int timeMax;
    float _timer;

    enum Dir
    {
        Right = 0,
        Left  = 1
    };
    Dir p_Dir = Dir.Right;

    ItemController.Foods p_Food = ItemController.Foods.normal;
    [SerializeField] bool bSpriteRender;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        // ���j���[��ʂł͂Ȃ���
        if (!GameRule.menuFlag)
        {
            GameObject item;
            if (!throwF && ActionFlag)
            {
                setRandom();
                setFood();

                prevRand = iRand;
                InstantiatePosition();
                getFood();
                throwF = true;
                item = Instantiate(prefabFood, foodPosition, Quaternion.identity);

                item.GetComponent<ItemController>().startPos = new Vector3(foodPosition.x, foodPosition.y, transform.position.z);
                item.GetComponent<ItemController>().endPos = new Vector2(ThrowPoint[iRand].transform.position.x, ThrowPoint[iRand].transform.position.y);
                item.GetComponent<ItemController>().CenterUp = new Vector2(Vector2.Lerp(new Vector3(foodPosition.x, foodPosition.y, transform.position.z), item.GetComponent<ItemController>().endPos, 1f).x, foodPosition.y + centerUp);
            }

            Timer();
        }
    }

    void Timer()
    {
        _timer += (1.0f / timeMax);

        if(_timer >= 1.0f)
        {
            _timer = 0f;
            throwF = false;
        }
    }

    void InstantiatePosition()
    {
        switch (p_Dir)
        {
            case Dir.Right:
                foodPosition = new Vector2(ThrowCharaPoint[0].transform.position.x, ThrowCharaPoint[0].transform.position.y);
                break;
            case Dir.Left:
                foodPosition = new Vector2(ThrowCharaPoint[1].transform.position.x, ThrowCharaPoint[1].transform.position.y);
                break;
        }

        if(p_Dir == Dir.Right && !throwF)
        { p_Dir = Dir.Left; }
        else if(p_Dir == Dir.Left && !throwF)
        { p_Dir = Dir.Right; }
    }

    // ������H�ו��̎�ނ��w�肷��
    void setFood()
    {
        // �H�ו��̎�ނ�ݒ肷��
        int iElement = Random.Range(0,3);

        // iElement�� Foods�̗񋓌^�ɃL���X�g����
        p_Food = (ItemController.Foods)iElement;
        switch (p_Food)
        {
            // �D���̐H�ו��̃v���t�@�u�I�u�W�F�N�g��ݒ肷��
            case ItemController.Foods.favorite:
                prefabFood = FoodObject[0];
                break;

            // ���ʂ̐H�ו��̃v���t�@�u�I�u�W�F�N�g��ݒ肷��
            case ItemController.Foods.normal:
                prefabFood = FoodObject[1];
                break;

            // ���̐H�ו��̃v���t�@�u�I�u�W�F�N�g��ݒ肷��
            case ItemController.Foods.disliked:
                prefabFood = FoodObject[2];
                break;
        }
        //Debug.Log("�H�ו��̎�ށF" + typeName);
    }

    void setRandom()
    {
        int iElement = Random.Range(0, 7);

        // ������n�_����ɏW�����Ȃ��悤�ɂ���
        if(iElement != prevRand)
        {
            // ������ʒu��ݒ肷��
            iRand = iElement;
        }
        else 
        {
            // ������������x�Ăяo���l��ύX����
            setRandom();
        }
    }

    public int getFood()
    {
        return (int)p_Food;
    }
    
}
