using UnityEngine;
using UnityEngine.SceneManagement; // ������ �������

public class ReturnButten : MonoBehaviour
{
    // ���� ������ ��� ������ �������
    public void OnReturnButtonPressed()
    {
        // ���� �� "Main Menu" �� ��� ������ ���� ���� ������ ����
        SceneManager.LoadScene("MainMenu"); // ������ "MainMenu" ���� ������ ������
    }
}
