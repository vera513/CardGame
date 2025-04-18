using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // مرجع للسلايدر و حقل النص
    public Slider volumeSlider;
    public InputField usernameInputField;

    // لتخزين القيم
    private float savedVolume = 1f;
    private string savedUsername = "";

    void Start()
    {
        // تحميل الإعدادات المحفوظة
        savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        savedUsername = PlayerPrefs.GetString("Username", "Player");

        // ضبط القيم في الواجهة
        volumeSlider.value = savedVolume;
        usernameInputField.text = savedUsername;

        // إضافة الاستماع للتغييرات
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        usernameInputField.onEndEdit.AddListener(OnUsernameChanged);
    }

    // لتغيير مستوى الصوت
    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value; // تغيير مستوى الصوت في اللعبة
        PlayerPrefs.SetFloat("Volume", value); // حفظ القيمة
    }

    // لتغيير اسم المستخدم
    public void OnUsernameChanged(string value)
    {
        savedUsername = value;
        PlayerPrefs.SetString("Username", savedUsername); // حفظ اسم المستخدم
    }

    // عند الضغط على زر "تم" لا يتم إغلاق النافذة أو اختفاء الزر
    public void OnDoneButtonClicked()
    {
        // فقط لحفظ الإعدادات بدون إغلاق النافذة أو تغيير حالتها
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetString("Username", usernameInputField.text);
    }
}
