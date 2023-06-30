using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FoodData : ScriptableObject
{
    //
    public string foodsName;

    //
    public SpriteRenderer foodSprite;

    //
    public int foodScore;

    //
    public AudioClip Sound;

    // FoodData �����݂��Ă���ꏊ�̃p�X
    public const string PATH = "FoodData";

    // FoodData �̎���
    private static FoodData _entity;
    public static FoodData Entity
    {
        get 
        {
            // ���A�N�Z�X���Ƀ��[�h����
            if(_entity == null)
            {
                _entity = Resources.Load<FoodData>(PATH);

                if (_entity == null)
                {
                    Debug.LogError(PATH + "not found");
                }
            }

            return _entity;
        }
    }

}
