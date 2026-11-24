using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiMenuManager : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("Map");
    }
    public void LoadGame()
    {
        //hola
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
