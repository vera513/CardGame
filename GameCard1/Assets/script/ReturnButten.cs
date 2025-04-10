using UnityEngine;
using UnityEngine.SceneManagement; // áÊÍãíá ÇáãÔÇåÏ

public class ReturnButten : MonoBehaviour
{
    // ÏÇáÉ ÇáÚæÏÉ Åáì ÇáãÔåÏ ÇáÑÆíÓí
    public void OnReturnButtonPressed()
    {
        // ÊÃßÏ Ãä "Main Menu" åæ ÇÓã ÇáãÔåÏ ÇáĞí ÊÑíÏ ÇáÚæÏÉ Åáíå
        SceneManager.LoadScene("MainMenu"); // ÇÓÊÈÏá "MainMenu" ÈÇÓã ÇáãÔåÏ ÇáİÚáí
    }
}
