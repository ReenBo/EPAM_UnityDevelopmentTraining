using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.GameMenu
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _gameMenu;

        private float _timeValue = 0;

        private bool IsPaused = false;

        protected void Update()
        {
            ChangerGameMenu();
        }

        public void ChangerGameMenu()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!IsPaused)
                {
                    _gameMenu.SetActive(true);
                    _timeValue = 0f;
                    IsPaused = true;
                }
                else
                {
                    _gameMenu.SetActive(false);
                    _timeValue = 1f;
                    IsPaused = false;
                }

                Time.timeScale = _timeValue;
            }
        }
    }
}
