using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuManager : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("Map");
    }
    public void LoadGame()
    {
        GameManager.Instance.LoadGame();
        SceneManager.LoadScene("Map");
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
