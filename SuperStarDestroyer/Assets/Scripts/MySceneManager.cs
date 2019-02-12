using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public void OpenScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
