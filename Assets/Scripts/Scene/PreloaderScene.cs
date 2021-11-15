using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ET.Scenes;
using ET.Core.LevelInfo;
using ET.UI.LoadingView;
using ET.Core.UIRoot;
using ET.UI.WindowTypes;
using UnityEngine.UI;

namespace ET.Scenes.Preloader
{
    public class PreloaderScene : MonoBehaviour
    {
        private AsyncOperation _loading = null;

        [SerializeField] private LoadingViewController _LoadingLineView;

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected void Start()
        {
            SceneManager.LoadSceneAsync(SceneIndex._MainMenu.ToString(), LoadSceneMode.Additive);
        }

        private static Action onLoaderCallback;

        public void Load(SceneIndex scene)
        {
            onLoaderCallback = () =>
            {
                StartCoroutine(AsyncLoading(scene));
            };

            LoaderCallback();
        }

        private void LoaderCallback()
        {
            if (onLoaderCallback != null)
            {
                onLoaderCallback();
                onLoaderCallback = null;
            }
        }

        private IEnumerator AsyncLoading(SceneIndex scene)
        {
            UIRoot.Instance.OpenWindow(WindowType.LOADING_SCREEN);

            _loading = SceneManager.LoadSceneAsync(SceneName.GameSession);

            _loading.allowSceneActivation = false;

            while (!_loading.isDone)
            {
                _LoadingLineView.LoadingLine.fillAmount += Mathf.Clamp01(1e-3f);

                if (_loading.progress >= 0.9f)
                {
                    _loading.allowSceneActivation = true;
                }
                yield return null;
            }
            yield return _loading;

            _loading = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);
            yield return _loading;


            var levelInfo = GameObject.FindGameObjectWithTag(Tags.LEVEL_INFO);
            InfoSceneObjects infoSceneObjects = levelInfo.GetComponent<InfoSceneObjects>();

            yield return GameManager.Instance.InitGame(infoSceneObjects);

            UIRoot.Instance.CloseWindow(WindowType.LOADING_SCREEN);

            if (_loading.isDone)
            {
                GameManager.Instance.GameSessionStatus(true);
            }
        }
    }
}
