using ET.Core.UIRoot;
using ET.Interface.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.UI.GameOverWindow
{
    public class GameOverWindow : MonoBehaviour, IUIScreenable
    {
        //[SerializeField] private GameObject _gameOverPrefab;

        private float _resetTimeGameLevel = 50f;

        public void Show()
        {
            gameObject.SetActive(true);

            StartCoroutine(ResettingTime());
        }

        public void Hide()
        {
            gameObject.SetActive(false);
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
                    Hide();
                    GameManager.Instance.SceneController.ReturnMainMenu();
                }
                yield return null;
            }
        }
    }
}
