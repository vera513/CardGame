using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGame : MonoBehaviour
{
    public Button[] cards;                  // ÃÒÑÇÑ ÇáÈØÇŞÇÊ
    public Sprite[] cardFrontImages;        // ÕæÑ ÇáæÌæå (ãÎÊáİÉ)
    public Sprite cardBackImage;            // ÕæÑÉ ÎáİíÉ æÇÍÏÉ áÌãíÚ ÇáÈØÇŞÇÊ

    private int[] cardValues;               // ÇáŞíã/ÇáãÄÔÑÇÊ áßá ÈØÇŞÉ (ãËáÇğ 1¡2¡1¡2)
    private bool[] cardRevealed;            // ÍÇáÉ ßá ÈØÇŞÉ (åá åí ãßÔæİÉ¿)
    private int revealedCardIndex = -1;     // áÊÊÈÚ Ãæá ÈØÇŞÉ Êã ßÔİåÇ
    private bool isPlayerTurn = true;       // ÏæÑ ÇááÇÚÈ (ããßä äØæÑå áÇÍŞğÇ áíßæä áÇÚÈíä)

    private bool isRevealingCards = false;  // ááÊÍßã İí ÊİÇÚá ÇáÈØÇŞÇÊ ÃËäÇÁ ÇáÇäÊÙÇÑ

    void Start()
    {
        InitializeLevel();
    }

    void InitializeLevel()
    {
        cardValues = new int[cards.Length];
        cardRevealed = new bool[cards.Length];

        // ÅÚÏÇÏ Şíã ÚÔæÇÆíÉ ááÈØÇŞÇÊ (ÃÒæÇÌ)
        for (int i = 0; i < cards.Length / 2; i++)
        {
            cardValues[i * 2] = i + 1;
            cardValues[i * 2 + 1] = i + 1;
        }

        ShuffleArray(cardValues);

        // ÅÚÏÇÏ ßá ÒÑ ááÈØÇŞÉ
        for (int i = 0; i < cards.Length; i++)
        {
            int index = i;
            cards[i].onClick.RemoveAllListeners();
            cards[i].onClick.AddListener(() => RevealCard(index));
            cards[i].GetComponent<Image>().sprite = cardBackImage; // ÈÏÁÇğ ÈÇáÎáİíÉ
            cardRevealed[i] = false;
        }
    }

    void RevealCard(int cardIndex)
    {
        if (cardRevealed[cardIndex] || isRevealingCards) return; // ÅĞÇ ßÇäÊ ÇáÈØÇŞÉ ãßÔæİÉ Ãæ ÅĞÇ ßÇä İí æÖÚ ÅÎİÇÁ ÇáÈØÇŞÇÊ

        cardRevealed[cardIndex] = true;

        // ÚÑÖ ÇáæÌå ÇáÕÍíÍ
        cards[cardIndex].GetComponent<Image>().sprite = cardFrontImages[cardValues[cardIndex] - 1];

        if (revealedCardIndex == -1)
        {
            revealedCardIndex = cardIndex;
        }
        else
        {
            if (cardValues[cardIndex] == cardValues[revealedCardIndex])
            {
                Debug.Log("Match found!");
            }
            else
            {
                StartCoroutine(HideCards(cardIndex, revealedCardIndex)); // ÇÈÏÃ İí ÅÎİÇÁ ÇáÈØÇŞÇÊ ÈÚÏ İÊÑÉ
            }

            revealedCardIndex = -1;
            isPlayerTurn = !isPlayerTurn;
        }
    }

    IEnumerator HideCards(int cardIndex1, int cardIndex2)
    {
        isRevealingCards = true;  // ÊİÚíá ÍÇáÉ ÅÎİÇÁ ÇáÈØÇŞÇÊ

        yield return new WaitForSeconds(1f);  // ÇáÇäÊÙÇÑ áİÊÑÉ ŞÕíÑÉ ŞÈá ÅÎİÇÁ ÇáÈØÇŞÇÊ

        cardRevealed[cardIndex1] = false;
        cardRevealed[cardIndex2] = false;

        cards[cardIndex1].GetComponent<Image>().sprite = cardBackImage;
        cards[cardIndex2].GetComponent<Image>().sprite = cardBackImage;

        isRevealingCards = false; // ÅÚÇÏÉ ÇáæÖÚ ÇáØÈíÚí ÈÚÏ ÅÎİÇÁ ÇáÈØÇŞÇÊ
    }

    void ShuffleArray(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            int rand = Random.Range(i, array.Length);
            int temp = array[i];
            array[i] = array[rand];
            array[rand] = temp;
        }
    }
}
