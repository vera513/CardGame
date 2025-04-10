using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // åĞÇ ÇáßæÏ ÓíÊã ÊäİíĞå ÚäÏ ÇáÖÛØ Úáì ÒÑ ÇáÎÑæÌ
    public void ExitApplication()
    {
        // åĞÇ ÇáÓØÑ íÛáŞ ÇáÊØÈíŞ Úáì ÌåÇÒ ÃäÏÑæíÏ
        Application.Quit();
        Debug.Log("Êã ÇáÖÛØ Úáì ÒÑ ÇáÎÑæÌ");  // ÑÓÇáÉ ÊÙåÑ İí Console

        // åĞÇ ÇáÓØÑ áä íßæä áå ÊÃËíÑ ÏÇÎá Unity Editor
        // İí ÍÇáÉ ÇÎÊÈÇÑ ÇááÚÈÉ İí ÇáãÍÑÑ (ÇáÜ Editor) İŞØ
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
