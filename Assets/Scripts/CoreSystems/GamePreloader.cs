using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePreloader : MonoBehaviour
{
    protected void Awake()
    {
        SceneManager.LoadScene("_Level_0_Start");
    }
}
