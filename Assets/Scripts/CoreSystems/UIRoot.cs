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

        public event Action<WindowType> onOpenWindow;
        public event Action<WindowType> onCloseWindow;

        private bool _isVisible = false;

        public Popup Popup { get => _popup; }
        public HUD HUD { get => _hUD; }

        protected void Awake()
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }

        protected void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!_isVisible) 
                {
                    onOpenWindow.Invoke(WindowType.PAUSE_MENU);
                }
                else
                {
                    onCloseWindow.Invoke(WindowType.PAUSE_MENU);
                }
            }
        }

        public void UpdateAfterLaunch(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void ReceiveStatusOfSubscribersHandler(bool status)
        {
            if (status)
            {
                onOpenWindow += OpenWindow;
                onCloseWindow += CloseWindow;
                _playerController.onPlayerDied += CloseWindow;
            }
            else
            {
                onOpenWindow -= OpenWindow;
                onCloseWindow -= CloseWindow;
                _playerController.onPlayerDied -= CloseWindow;
            }
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
