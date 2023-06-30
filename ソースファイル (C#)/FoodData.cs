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

    // FoodData が存在している場所のパス
    public const string PATH = "FoodData";

    // FoodData の実体
    private static FoodData _entity;
    public static FoodData Entity
    {
        get 
        {
            // 初アクセス時にロードする
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
