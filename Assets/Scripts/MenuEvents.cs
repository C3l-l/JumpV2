
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuEvents : MonoBehaviour
{
    //public void LoadLevel(int index)
    //{
        //Time.timeScale = 1;
        //SceneManager.LoadScene(index);
    //}

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
