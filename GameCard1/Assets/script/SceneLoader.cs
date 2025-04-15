using UnityEngine;
using UnityEngine.SceneManagement; // · „ﬂÌ‰  Õ„Ì· «·„‘«Âœ

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
