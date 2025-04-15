using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // مرجع للأزرار
    public Button startGameButton;
    public Button continueGameButton;
    public Button settingsButton;
    public Button exitButton;
    public Button closeSettingsButton;

    // مرجع لواجهة الإعدادات
    public GameObject settingsPanel;

    // Start is called before the first frame update
    void Start()
    {
        // إضافة أحداث للأزرار
        startGameButton.onClick.AddListener(StartNewGame);
        continueGameButton.onClick.AddListener(ContinueGame);
        settingsButton.onClick.AddListener(OpenSettings);
        exitButton.onClick.AddListener(ExitGame);
        closeSettingsButton.onClick.AddListener(CloseSettings);
    }

    // لبدء لعبة جديدة
    public void StartNewGame()
    {
        // هنا يمكنك إضافة الكود للانتقال إلى المشهد الأول للعبة
        SceneManager.LoadScene("Level"); // استبدل "Level" باسم المشهد الخاص باللعبة
    }

    // لاستئناف اللعبة (إذا كان لديك نظام حفظ التقدم)
    public void ContinueGame()
    {
        // هنا يمكنك إضافة الكود لاستئناف التقدم
        SceneManager.LoadScene("Level"); // استبدل "Level" باسم المشهد الخاص باللعبة
    }

    // لفتح نافذة الإعدادات
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);  // إظهار نافذة الإعدادات
    }

    // لإغلاق نافذة الإعدادات
    public void CloseSettings()
    {
        settingsPanel.SetActive(false); // إخفاء نافذة الإعدادات
    }

    // لخروج اللاعب من اللعبة
    public void ExitGame()
    {
        Application.Quit();
    }
}

