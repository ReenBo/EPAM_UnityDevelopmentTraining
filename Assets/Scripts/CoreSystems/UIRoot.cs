using ET.Interface.UI;
using ET.Player;
using ET.Player.UI.ExperienceView;
using ET.Player.UI.StatsView;
using ET.UI.GameOverWindow;
using ET.UI.PauseMenu;
using ET.UI.SkillsView;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace ET.Core.UIRoot
{
    public enum WindowType
    {
        PAUSE_MENU,
        GAME_OVER,
    }

    public class UIRoot : MonoBehaviour
    {
        private static UIRoot _instance = null;

        public static UIRoot Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("UIRoot is NULL");
                }

                return _instance;
            }
        }

        IUIScreenable _pauseMenuScreen;
        IUIScreenable _gameOverScreen;

        private Dictionary<WindowType, IUIScreenable> _UIObjects;

        [Header("References to the Player Components")]
        private PlayerController _playerController = null;

        [Header("References to the UI Components")]
        [SerializeField] private PauseMenuWindow _pauseMenuWindow;
        [SerializeField] private GameOverWindow _gameOverWindow;

        [SerializeField] private PlayerStatsView _playerStatsView;
        [SerializeField] private PlayerExperienceView _playerExperienceView;
        [SerializeField] private PlayerSkillsView _playerSkillsView;

        private bool _isVisible = false;

        protected void Awake()
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }

        protected void Start()
        {
            _pauseMenuScreen = _pauseMenuWindow;
            _gameOverScreen = _gameOverWindow;

            _UIObjects = new Dictionary<WindowType, IUIScreenable>
            {
                { WindowType.PAUSE_MENU, _pauseMenuScreen },
                { WindowType.GAME_OVER, _gameOverScreen }
            };
        }

        public Action<WindowType> onOpenPauseMenu;
        public Action<WindowType> onClosePauseMenu;

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_isVisible)
                {
                    onOpenPauseMenu.Invoke(WindowType.PAUSE_MENU);
                }
                else
                {
                    onClosePauseMenu.Invoke(WindowType.PAUSE_MENU);
                }
            }
        }

        public void UpdateAfterLaunch(PlayerController playerController)
        {
            _playerController = playerController;
            //_playerController.onPlayerDied += OpenWindow; !!!CRASH!!!

            onOpenPauseMenu += OpenWindow;
            onClosePauseMenu += CloseWindow;
        }

        public void OpenWindow(WindowType window)
        {
            if (!_isVisible)
            {
                _UIObjects[window].Show();
                _isVisible = true;
            }
        }

        public void CloseWindow(WindowType window)
        {
            if (_isVisible)
            {
                _UIObjects[window].Hide();
                _isVisible = false;
            }
        }

        public void CloseAllWindow()
        {
            if (_isVisible)
            {
                foreach (var pair in _UIObjects.Values)
                {
                    pair.Hide();
                }
                _isVisible = false;
            }
        }
    }
}
