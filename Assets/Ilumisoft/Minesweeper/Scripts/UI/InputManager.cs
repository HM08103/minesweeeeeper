using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    InputField inputField;

    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        //inputfieldコンポーネントの取得、初期化メソッド実行
        inputField = GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //入力値を取得してLogに出力してから初期化
    public void InputLogger(){
        string inputValue = inputField.text;
        Debug.Log(inputValue);
    }
}
