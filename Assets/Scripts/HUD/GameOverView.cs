using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.UI.GameOverView
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverTitle;

        private float _resetTimeGameLevel = 50f;

        public void GameOver()
        {
            //Instantiate(_gameOverTitle, new Vector2(0, 0), Quaternion.identity);
            _gameOverTitle.SetActive(true);

            StartCoroutine(ResettingTime());
        }

        private IEnumerator ResettingTime()
        {
            float timer = _resetTimeGameLevel;

            while (true)
            {
                if (timer > 0f)
                {
                    timer -= Time.fixedDeltaTime;
                }
                else
                {
                    GameManager.Instance.SceneController.ReturnMainMenu();
                }
                yield return null;
            }
        }
    }
}
