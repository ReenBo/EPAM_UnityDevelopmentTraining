using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ET.Scene
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverTitle;

        public enum Scene
        {
            _Level_0_Start,
            _Level_0_Loading,
            _Level_1
        }

        private static Action onLoaderCallback;

        public void StartGame()
        {
            Load(Scene._Level_1);
        }

        public void GameOver()
        {
            Instantiate(_gameOverTitle, new Vector2(0, 0), Quaternion.identity);
            _gameOverTitle.SetActive(true);

            StartCoroutine(ResettingTime());
        }

        public void Restart()
        {
            Load(Scene._Level_1);
        }

        public void ReturnMainMenu()
        {
            Load(Scene._Level_0_Start);
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
            Time.timeScale = 1f;
        }

        public static void LoaderCallback()
        {
            if (onLoaderCallback != null)
            {
                onLoaderCallback();
                onLoaderCallback = null;
            }
        }

        private IEnumerator ResettingTime()
        {
            float timer = 30f;

            while (true)
            {
                if (timer > 0f)
                {
                    timer -= Time.fixedDeltaTime;
                }
                else
                {
                    ReturnMainMenu();
                }
                yield return null;
            }
        }
    }
}
