using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.GameMenu
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _gameMenu;

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
                    Time.timeScale = 0f;
                    IsPaused = true;
                }
                else
                {
                    _gameMenu.SetActive(false);
                    Time.timeScale = 1f;
                    IsPaused = false;
                }
            }
        }
    }
}
