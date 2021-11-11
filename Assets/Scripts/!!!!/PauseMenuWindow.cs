using ET.Core.UIRoot;
using ET.Interface.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.UI.PauseMenu
{
    public class PauseMenuWindow : MonoBehaviour, IUIScreenable
    {
        //[SerializeField] private GameObject _gameMenuPrefab;

        private bool _isPaused = false;

        public void Show()
        {
            if (!_isPaused)
            {
                gameObject.SetActive(true);
                Time.timeScale = 0f;
                _isPaused = true;
            }
        }

        public void Hide()
        {
            if(_isPaused)
            {
                gameObject.SetActive(false);
                Time.timeScale = 1f;
                _isPaused = false;
            }
        }

        //private void OnDestroy()
        //{
        //    Time.timeScale = 1f;
        //}
    }
}
