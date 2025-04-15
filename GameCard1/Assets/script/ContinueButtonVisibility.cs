using UnityEngine;

public class ContinueButtonVisibility : MonoBehaviour
{
    public GameObject continueButton; // اسحب زر "استمرار" هنا كـ GameObject

    void Start()
    {
        // نتحقق إذا فيه بيانات محفوظة (مثلاً اسم المرحلة المحفوظة)
        if (PlayerPrefs.HasKey("LastLevel"))
        {
            continueButton.SetActive(true); // إظهار الزر
        }
        else
        {
            continueButton.SetActive(false); // إخفاء الزر
        }
    }
}

