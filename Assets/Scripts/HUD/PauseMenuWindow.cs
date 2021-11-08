using ET.Core.UIRoot;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.UI.PauseMenu
{
    public class PauseMenuWindow : MonoBehaviour
    {
        private WindowType _windowType = WindowType.PAUSE_MENU;

        [SerializeField] private GameObject _gameMenuPrefab;

        private bool _isPaused = false;

        protected void Update()
        {
            ChangerGameMenu();
        }

        public void ChangerGameMenu()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_isPaused)
                {
                    //_gameMenuPrefab.SetActive(true);
                    UIRoot.Instance.OpenWindow(_windowType);
                    Time.timeScale = 0f;
                    _isPaused = true;
                }
                else
                {
                    //_gameMenuPrefab.SetActive(false);
                    UIRoot.Instance.CloseWindow(_windowType);
                    Time.timeScale = 1f;
                    _isPaused = false;
                }
            }
        }

        private void OnDestroy()
        {
            Time.timeScale = 1f;
        }
    }
}
