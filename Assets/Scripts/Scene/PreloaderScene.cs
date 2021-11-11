using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using ET.Scenes;

namespace ET.Scenes.Preloader
{
    public class PreloaderScene : MonoBehaviour
    {
        private AsyncOperation loading;

        [SerializeField] private GameObject _preloaderUI;

        //private readonly string _preLevel = "_PreLevel";
        private readonly string _gameSession = "_GameSession";

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
            _preloaderUI.SetActive(true);

            loading = SceneManager.LoadSceneAsync(_gameSession);
            yield return loading;

            loading = SceneManager.LoadSceneAsync(scene.ToString(), LoadSceneMode.Additive);
            yield return loading;

            var levelInfo = GameObject.FindGameObjectWithTag(Tags.LEVEL_INFO);
            InfoSceneObjects infoSceneObjects = levelInfo.GetComponent<InfoSceneObjects>();

            yield return GameManager.Instance.InitGame(infoSceneObjects); //

            _preloaderUI.SetActive(false);

            if (loading.isDone)
            {
                GameManager.Instance.StartGame(true);
            }
        }
    }
}
