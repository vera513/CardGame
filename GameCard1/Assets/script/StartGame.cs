using UnityEngine;
using UnityEngine.SceneManagement;  // ÇÓÊíÑÇÏ SceneManager

public class StartGame : MonoBehaviour
{
    // ÏÇáÉ íÊã ÇÓÊÏÚÇÄåÇ ÚäÏ ÇáÖÛØ Úáì ÒÑ "ÇÈÏÃ"
    public void StartGameScene()
    {
        // ÊÍãíá ÇáãÔåÏ ÇáĞí íÍãá ÇÓã "Level"
        SceneManager.LoadScene("Level");
        Debug.Log("Êã ÇáÇäÊŞÇá Åáì ãÔåÏ ÇáãÓÊæì");
    }
}

