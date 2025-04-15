using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName; // متغير يمكن إدخاله من Unity Inspector

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))  // التأكد من أن اسم المشهد ليس فارغًا
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("اسم المشهد غير محدد! يرجى إدخاله في الـ Inspector.");
        }
    }
}