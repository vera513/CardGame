using UnityEngine;
using UnityEngine.UI;

public class ArabicTextReverser : MonoBehaviour
{
    public InputField nameInput;  // ��� �������
    public Text displayName;      // ���� �������

    // ���� ���� ���� �� ��� ����� ������ ��� ���
    string ReverseArabic(string input)
    {
        string reversed = "";

        // ���� ���� ���� ����� �����
        for (int i = input.Length - 1; i >= 0; i--)
        {
            reversed += input[i];
        }

        return reversed;
    }

    // ���� ������ ���� ��� ����
    public void UpdateReversedText()
    {
        string rawName = nameInput.text;  // ��� ���� �� ��� �������
        displayName.text = ReverseArabic(rawName);  // ��� ���� �������
    }
}

