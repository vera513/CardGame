using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    // مرجع للأزرار و السلايدر
    public Slider volumeSlider;
    public Toggle fullscreenToggle;

    // لتخزين القيم
    private float savedVolume = 1f;
    private bool savedFullscreen = true;

    void Start()
    {
        // تحميل الإعدادات المحفوظة
        savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        savedFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;

        // ضبط القيم في الواجهة
        volumeSlider.value = savedVolume;
        fullscreenToggle.isOn = savedFullscreen;

        // إضافة الاستماع للتغييرات
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
    }

    // لتغيير مستوى الصوت
    void OnVolumeChanged(float value)
    {
        AudioListener.volume = value; // تغيير مستوى الصوت في اللعبة
        PlayerPrefs.SetFloat("Volume", value); // حفظ القيمة
    }

    // لتغيير وضع الشاشة الكاملة
    void OnFullscreenChanged(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0); // حفظ القيمة
    }
}

