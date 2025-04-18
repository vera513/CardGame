using UnityEngine;
using UnityEngine.UI;

public class ArabicTextReverser : MonoBehaviour
{
    public InputField nameInput;  // ÍŞá ÇáÅÏÎÇá
    public Text displayName;      // ÇáäÕ ÇáãÚÑæÖ

    // ÏÇáÉ áÚßÓ ÇáäÕ ãÚ ÌÚá ÇáÍÑİ ÇáãÏÎá Ãæá ÍÑİ
    string ReverseArabic(string input)
    {
        string reversed = "";

        // ÍáŞÉ áÚßÓ ÇáäÕ ÍÑİğÇ ÍÑİğÇ
        for (int i = input.Length - 1; i >= 0; i--)
        {
            reversed += input[i];
        }

        return reversed;
    }

    // ÏÇáÉ áÊÍÏíË ÇáäÕ ÈÚÏ ŞáÈå
    public void UpdateReversedText()
    {
        string rawName = nameInput.text;  // ÃÎĞ ÇáäÕ ãä ÍŞá ÇáÅÏÎÇá
        displayName.text = ReverseArabic(rawName);  // ÚÑÖ ÇáäÕ ÇáãÚßæÓ
    }
}

