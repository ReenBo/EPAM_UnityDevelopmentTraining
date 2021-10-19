using ET.Scenes.Preloader;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ET.Scenes
{
    public class SceneController : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverTitle;
        [SerializeField] private PreloaderScene _preLoader;

        private static Action onLoaderCallback;

        public void Load(Scenes scene)
        {
            onLoaderCallback = () =>
            {
                SceneManager.LoadSceneAsync(scene.ToString());
            };

            //_preLoader.UploadPreScene();
            LoaderCallback();

            Time.timeScale = 1f;
        }

        private void LoaderCallback()
        {
            if (onLoaderCallback != null)
            {
                onLoaderCallback();
                onLoaderCallback = null;
            }
        }

        public void StartGame()
        {
            Load(Scenes._Level_1);
        }

        public void GameOver()
        {
            Instantiate(_gameOverTitle, new Vector2(0, 0), Quaternion.identity);
            _gameOverTitle.SetActive(true);

            StartCoroutine(ResettingTime());
        }

        public void Restart()
        {
            Load(Scenes._Level_1);
        }

        public void ReturnMainMenu()
        {
            Load(Scenes._Level_0_MainMenu);
        }

        public void EndGame()
        {
            Application.Quit();
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
