using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MemoryGame : MonoBehaviour
{
    public Button[] cards;                  // ����� ��������
    public Sprite[] cardFrontImages;        // ��� ������ (������)
    public Sprite cardBackImage;            // ���� ����� ����� ����� ��������

    private int[] cardValues;               // �����/�������� ��� ����� (����� 1�2�1�2)
    private bool[] cardRevealed;            // ���� �� ����� (�� �� �����ɿ)
    private int revealedCardIndex = -1;     // ����� ��� ����� �� �����
    private bool isPlayerTurn = true;       // ��� ������ (���� ����� ������ ����� ������)

    private bool isRevealingCards = false;  // ������ �� ����� �������� ����� ��������

    void Start()
    {
        InitializeLevel();
    }

    void InitializeLevel()
    {
        cardValues = new int[cards.Length];
        cardRevealed = new bool[cards.Length];

        // ����� ��� ������� �������� (�����)
        for (int i = 0; i < cards.Length / 2; i++)
        {
            cardValues[i * 2] = i + 1;
            cardValues[i * 2 + 1] = i + 1;
        }

        ShuffleArray(cardValues);

        // ����� �� �� �������
        for (int i = 0; i < cards.Length; i++)
        {
            int index = i;
            cards[i].onClick.RemoveAllListeners();
            cards[i].onClick.AddListener(() => RevealCard(index));
            cards[i].GetComponent<Image>().sprite = cardBackImage; // ����� ��������
            cardRevealed[i] = false;
        }
    }

    void RevealCard(int cardIndex)
    {
        if (cardRevealed[cardIndex] || isRevealingCards) return; // ��� ���� ������� ������ �� ��� ��� �� ��� ����� ��������

        cardRevealed[cardIndex] = true;

        // ��� ����� ������
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
                StartCoroutine(HideCards(cardIndex, revealedCardIndex)); // ���� �� ����� �������� ��� ����
            }

            revealedCardIndex = -1;
            isPlayerTurn = !isPlayerTurn;
        }
    }

    IEnumerator HideCards(int cardIndex1, int cardIndex2)
    {
        isRevealingCards = true;  // ����� ���� ����� ��������

        yield return new WaitForSeconds(1f);  // �������� ����� ����� ��� ����� ��������

        cardRevealed[cardIndex1] = false;
        cardRevealed[cardIndex2] = false;

        cards[cardIndex1].GetComponent<Image>().sprite = cardBackImage;
        cards[cardIndex2].GetComponent<Image>().sprite = cardBackImage;

        isRevealingCards = false; // ����� ����� ������� ��� ����� ��������
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
