using ET.Player;
using ET.Player.UI.ExperienceView;
using ET.Player.UI.StatsView;
using ET.UI.GameOverWindow;
using ET.UI.PauseMenu;
using ET.UI.SkillsView;
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

        private Dictionary<WindowType, GameObject> _UIObjects = new Dictionary<WindowType, GameObject>();

        [Header("References to the Player Components")]
        private PlayerController _playerController;

        [Header("References to the UI Components")]
        [SerializeField] private PauseMenuWindow _pauseMenuWindow;
        [SerializeField] private GameOverWindow _gameOverWindow;

        [SerializeField] private PlayerStatsView _playerStatsView;
        [SerializeField] private PlayerExperienceView _playerExperienceView;
        [SerializeField] private PlayerSkillsView _playerSkillsView;

        public PauseMenuWindow PauseMenuWindow { get => _pauseMenuWindow; }
        public GameOverWindow GameOverWindow { get => _gameOverWindow; }

        public PlayerController PlayerController { get => _playerController; set => _playerController = value; }

        protected void Awake()
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);

            _UIObjects.Add(WindowType.PAUSE_MENU, _pauseMenuWindow.gameObject);
            _UIObjects.Add(WindowType.GAME_OVER, _gameOverWindow.gameObject);

        }

        public void UpdateAfterLaunch(PlayerController playerController)
        {
            _playerController = playerController;
            _playerController.onPlayerDied += _gameOverWindow.FinishGame;
        }

        public void OpenWindow(WindowType window)
        {
            foreach (var pair in _UIObjects)
            {
                if(window == pair.Key)
                {
                    pair.Value.SetActive(true);



                    //var valueType = pair.Value.GetType();
                    //MethodInfo[] methods = valueType.GetMethods();
                    //methods[0].Invoke(pair.Value, methods);
                }
            }
        }

        public void CloseWindow(WindowType window)
        {
            foreach (var pair in _UIObjects)
            {
                if (window == pair.Key)
                {
                    pair.Value.SetActive(false);
                }
            }
        }
    }
}
