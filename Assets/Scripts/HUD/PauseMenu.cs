using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.GameMenu
{
    public class PauseMenu : MonoBehaviour
    {
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
                    _gameMenuPrefab.SetActive(true);
                    Time.timeScale = 0f;
                    _isPaused = true;
                }
                else
                {
                    _gameMenuPrefab.SetActive(false);
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
