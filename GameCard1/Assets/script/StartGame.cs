using UnityEngine;
using UnityEngine.SceneManagement;  // ������� SceneManager

public class StartGame : MonoBehaviour
{
    // ���� ��� ��������� ��� ����� ��� �� "����"
    public void StartGameScene()
    {
        // ����� ������ ���� ���� ��� "Level"
        SceneManager.LoadScene("Level");
        Debug.Log("�� �������� ��� ���� �������");
    }
}

