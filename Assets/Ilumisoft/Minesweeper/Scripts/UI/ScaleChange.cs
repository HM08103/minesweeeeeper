using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChange : MonoBehaviour
{
    //-----ピンチ操作用宣言-----
    //カメラ視覚の範囲
    // float viewMin = 20.0f;
    // float viewMax = 60.0f;
    float vMin = 0.5f;
    float vMax = 3.0f;

    //直前2点間の距離
    private float backDist = 0.0f;
    
    //初期値
    float view = 60.0f;
    float v = 1.0f;

    //-----スワイプ用変数宣言-----
    float x_speed = 0;
    float y_speed = 0;
    Vector2 startPos;

    // Update is called once per frame
    void Update()
    {
        //マルチタッチかどうか？
        if(Input.touchCount >= 2){
            //タッチしている2点を取得
            Touch t1 = Input.GetTouch(0);
            Touch t2 = Input.GetTouch(1);

            //2点タッチ開始時の距離を記憶
            if(t2.phase == TouchPhase.Began){
                backDist = Vector2.Distance(t1.position, t2.position);
            } else if(t1.phase == TouchPhase.Moved && t2.phase == TouchPhase.Moved){
                //タッチ位置移動後、長さを再測、前回の距離からの相対値を取得。
                float newDist = Vector2.Distance(t1.position, t2.position);
                view = view + (backDist - newDist) / 1000.0f;
                v = v + (newDist - backDist) / 10000.0f;

                //限界値をオーバーしたときの処理
                if(v > vMax){
                    v = vMax;
                } else if(v < vMin){
                    v = vMin;
                }

                //相対値が変更した場合、カメラに相対値を反映させる
                if(v != 0){
                    GameObject.Find("Tile Container").transform.localScale = new Vector3(v, v, 1.0f);
                }
            }
        }
        MoveSwipe();
    }

    //スワイプ操作
    void MoveSwipe(){
        //マウス左クリックの時
        if(Input.GetMouseButtonDown(0)){
            //クリックした座標
            this.startPos = Input.mousePosition;
        } else if(Input.GetMouseButton(0)){
            //マウスを離した座標
            Vector2 endPos = Input.mousePosition;
            float x_swipeLength = endPos.x - this.startPos.x;
            float y_swipeLength = endPos.y - this.startPos.y;

            //スワイプの長さを速度に変換
            this.x_speed = x_swipeLength / 250.0f;
            this.y_speed = y_swipeLength / 250.0f;
        }

        //オブジェクトを動かす
        transform.Translate(this.x_speed, this.y_speed, 0);

        //減速
        this.x_speed *= 0.98f;
        this.y_speed *= 0.98f;
        this.startPos = Input.mousePosition;
    }
}
