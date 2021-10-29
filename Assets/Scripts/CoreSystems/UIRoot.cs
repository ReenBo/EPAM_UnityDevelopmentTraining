using ET.Player.UI.ExperienceView;
using ET.Player.UI.StatsView;
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

        protected void Awake()
        {
            _instance = this;

            DontDestroyOnLoad(gameObject);
        }

        [Header("References to GameObjects")]
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _gameOver;
        [SerializeField] private GameObject _hUD;

        //[Header("References to Components")]

        public void OpenWindow(object obj)
        {

        }

        public void Show()
        {

        }
    }
}
