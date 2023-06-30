using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creditControl : MonoBehaviour
{
        
    [SerializeField, Header("カメラオブジェクト")] GameObject _camera;               // カメラのオブジェクトを管理する変数
    [SerializeField, Header("カメラの移動位置")] GameObject [] _cameraPosition;      // カメラの位置を管理する変数
    [SerializeField, Header("ボタンオブジェクト")] GameObject [] _buttonObj;         // ボタンオブジェクトを管理する変数
    [SerializeField, Header("クレジットのテキスト")] Text _creditText;               // テキストオブジェクトを管理する変数
    [SerializeField, Header("ボタンの文字")] string[] _text;                         // テキストのオブジェクトの配列を管理する変数

    AudioSource audio;              // SEのコントローラーを管理する変数
    public AudioClip clip;          // 効果音を管理する変数
    Animator animator;              // アニメーションのコントローラーを管理する変数

    public enum TitleType
    {
        title = 0,
        credit,
    };
    [SerializeField,Header("タイトルシーンの状態")]TitleType _title;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Unity上で呼び出すことができる関数
    // クリック時に呼び出す関数
    public void _OnClick()
    {
        audio.PlayOneShot(clip);                    // 効果音を鳴らす
        animator.SetBool("creditClick", true);      // アニメーションを再生する
        Invoke("_cameraPos", 0.5f);                 // 時間差でカメラの位置を変更する
    }

    // ゲームオブジェクトを非表示にするメソッド
    private void _setActive(int _loopCount , bool _bActive)
    {
        // 配列の数分ループを回す
        for(int i =0;i<_loopCount;i++)
        {
            // ゲームオブジェクトを非表示にする
            _buttonObj[i].SetActive(_bActive);
        }
    }

    // カメラの位置を更新するメソッド
    private void _cameraPos()
    {
        // タイトルのシーンからクレジットへ変更する時の処理
        if (_title == TitleType.title)
        {
            _title = TitleType.credit;
            _setActive(_buttonObj.Length, false);               // オブジェクトを非表示にする
            _creditText.text = _text[(int)_title].ToString();   // 文字を変更する
        }
        // クレジットのシーンからタイトルへ変更する時の処理
        else if (_title == TitleType.credit)
        {
            _title = TitleType.title;
            _setActive(_buttonObj.Length, true);                // オブジェクトを非表示にする
            _creditText.text = _text[(int)_title].ToString();   // 文字を変更する
        }

        // カメラの位置を更新する
        _camera.transform.position = _cameraPosition[(int)_title].transform.position;
        
        // アニメーションを動かす
        animator.SetBool("creditClick", false);
    }
}
