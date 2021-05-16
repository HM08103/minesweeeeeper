using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//名前空間Ilumisoft/Minesweeper/UI直下にLoadSceneButtonを作成する
namespace Ilumisoft.Minesweeper.UI
{
    [RequireComponent(typeof(Button))]
    public class LoadSceneButton : MonoBehaviour
    {
        //変数をシリアライゼーションする。インスペクタウィンドウで編集可能になる
        [SerializeField]
        //string型SceneName変数に読み取り専用の空文字列を代入
        string sceneName = string.Empty;
        //シーンローダー・ボタンを使いやすく小文字に？
        SceneLoader sceneLoader;

        Button button;

        //awakeメソッド：startよりも早く呼び出される
        private void Awake()
        {
            //シーンローダーにシーンローダー型で最初に見つけたアクティブなオブジェクトを返す
            sceneLoader = FindObjectOfType<SceneLoader>();

            //ボタンにボタンコンポーネントを代入
            button = GetComponent<Button>();
            //ボタンがクリックされたときにOnButtonClickメソッドを呼び出す
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            //もしシーンローダーがnullじゃない場合
            if (sceneLoader != null)
            {
                //シーンローダークラスのロードシーンメソッドをsceneNameを引数にして実行する
                sceneLoader.LoadScene(sceneName);
            }
            else
            {
                //シーンローダーがnullの場合、sceneNameのシーンをロードする
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}