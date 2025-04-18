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

        // التحقق مما إذا كانت هناك بيانات محفوظة
        if (PlayerPrefs.HasKey("SavedLevel")) // "SavedLevel" هو اسم المفتاح لحفظ المستوى
        {
            continueGameButton.gameObject.SetActive(true); // إظهار زر الاستئناف
        }
        else
        {
            continueGameButton.gameObject.SetActive(false); // إخفاء زر الاستئناف
        }
    }

    // لبدء لعبة جديدة
    public void StartNewGame()
    {
        PlayerPrefs.DeleteAll(); // مسح كل بيانات الحفظ القديمة
        SceneManager.LoadScene("Level"); // تحميل مشهد البداية من جديد
    }

    // لاستئناف اللعبة
    public void ContinueGame()
    {
        SceneManager.LoadScene("Level"); // تحميل المشهد السابق من حيث تم الحفظ
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
    public void GoToStudio()
    {
        SceneManager.LoadScene("StudioScene");
    }

}
