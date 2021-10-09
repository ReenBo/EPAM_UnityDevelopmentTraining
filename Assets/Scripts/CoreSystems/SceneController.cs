using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using ET;
using ET.Player;

namespace ET.Scene
{
    public class SceneController : MonoBehaviour
    {
        public enum Scene
        {
            _Level_0_Start,
            _Level_0_Loading,
            _Level_1
        }

        private PlayerController _playerController = null;

        private static Action onLoaderCallback;

        protected void Start()
        {
            _playerController = GetComponent<PlayerController>();
        }

        public void StartGame()
        {
            Load(Scene._Level_1);
            //LoaderCallback();
        }

        public void GameOver()
        {
            if (_playerController.PlayerState == Player.States.PLAYER_STATE.IS_DEAD)
            {
                Time.timeScale = 0f;
                Debug.Log("GAME_OVER");
                // OnVisible string "GameOver"
                Restart();
            }
        }

        public void Restart()
        {
            Load(Scene._Level_1);
            Time.timeScale = 1f;
            //LoaderCallback();
        }

        public void ReturnMainMenu()
        {
            Load(Scene._Level_0_Start);
            //LoaderCallback();
        }

        public void EndGame()
        {
            Application.Quit();
        }

        public static void Load(Scene scene)
        {
            onLoaderCallback = () =>
            {
                SceneManager.LoadSceneAsync(scene.ToString());
            };

            SceneManager.LoadScene(Scene._Level_0_Loading.ToString());
            LoaderCallback();
        }

        public static void LoaderCallback()
        {
            if (onLoaderCallback != null)
            {
                onLoaderCallback();
                onLoaderCallback = null;
            }
        }
    }
}
