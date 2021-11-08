using ET.Core.UIRoot;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.UI.GameOverWindow
{
    public class GameOverWindow : MonoBehaviour
    {
        private WindowType _windowType = WindowType.GAME_OVER;

        [SerializeField] private GameObject _gameOverTitle;

        private float _resetTimeGameLevel = 50f;

        public void FinishGame()
        {
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
