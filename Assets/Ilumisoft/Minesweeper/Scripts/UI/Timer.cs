using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Ilumisoft.Minesweeper.UI
{
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        Text text = null;

        public bool IsStarted = false;
        float elapsedTime = 0.0f;

        void Update()
        {
            if(SceneManager.GetActiveScene().name == "Game Custom" && IsStarted == false){
                return;
            } else if(IsStarted == true) {
            elapsedTime += Time.deltaTime;
            text.text = GetTime();
            } else {
            elapsedTime += Time.deltaTime;
            text.text = GetTime();
            }
        }

        public string GetTime()
        {
            string minutes = Mathf.Floor(elapsedTime / 60).ToString("00");
            string seconds = (elapsedTime % 60).ToString("00");

            return minutes + ":" + seconds;
        }
    }
}