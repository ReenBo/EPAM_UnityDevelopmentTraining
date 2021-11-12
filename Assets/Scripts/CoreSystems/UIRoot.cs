using ET.Interface.UI;
using ET.Player;
using ET.UI.HUD;
using ET.UI.LoadingView;
using ET.UI.Popups;
using ET.UI.SkillsView;
using ET.UI.WindowTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Core.UIRoot
{
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

        [Header("References to the Player Components")]
        private PlayerController _playerController = null;

        [Header("References to the UI Components")]
        [SerializeField] private Popup _popup;
        [SerializeField] private HUD _hUD;

        private bool _isVisible = false;

        public Popup Popup { get => _popup; }
        public HUD HUD { get => _hUD; }

        protected void Awake()
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public event Action<WindowType> onOpenPauseMenu;
        public event Action<WindowType> onClosePauseMenu;

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
                _popup.UIObjects[window].Show();
                _isVisible = true;
            }
        }

        public void CloseWindow(WindowType window)
        {
            if (_isVisible)
            {
                _popup.UIObjects[window].Hide();
                _isVisible = false;
            }
        }

        public void CloseAllWindow()
        {
            if (_isVisible)
            {
                foreach (var pair in _popup.UIObjects.Values)
                {
                    pair.Hide();
                }
                _isVisible = false;
            }
        }
    }
}
