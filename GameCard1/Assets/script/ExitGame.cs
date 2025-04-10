using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // ��� ����� ���� ������ ��� ����� ��� �� ������
    public void ExitApplication()
    {
        // ��� ����� ���� ������� ��� ���� �������
        Application.Quit();
        Debug.Log("�� ����� ��� �� ������");  // ����� ���� �� Console

        // ��� ����� �� ���� �� ����� ���� Unity Editor
        // �� ���� ������ ������ �� ������ (��� Editor) ���
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
