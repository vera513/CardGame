using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StudioManager : MonoBehaviour
{
    public Transform cardGrid;                // ������ Content ���� Scroll View
    public GameObject cardPrefab;             // Prefab ����� ��� Image ���
    public Sprite[] cardFrontImages;          // ��� �������� ������� (���� �������)
    public Sprite lockedCardSprite;           // ���� ������ �� ����� �������� ��� ��������

    public GameObject fullImagePanel;         // Panel ���� ������� ������ ������
    public Image fullImageDisplay;            // ���� ������� ���� Panel

    void Start()
    {
        LoadAllCards();
        if (fullImagePanel != null)
            fullImagePanel.SetActive(false); // ����� ����� ����� ��������
    }

    void LoadAllCards()
    {
        for (int i = 0; i < cardFrontImages.Length; i++)
        {
            int index = i; // ��� ����� ����� �����

            GameObject cardObj = Instantiate(cardPrefab, cardGrid);
            Image cardImage = cardObj.GetComponent<Image>();

            // ����� Button �������� ��� ��� ��� �����
            Button cardButton = cardObj.GetComponent<Button>();
            if (cardButton == null)
            {
                cardButton = cardObj.AddComponent<Button>();  // ����� Button ��� �������
            }

            // ����� ������ �������� �������
            if (PlayerPrefs.GetInt("UnlockedCard_" + i, 0) == 1)
            {
                cardImage.sprite = cardFrontImages[i]; // ������� ������
                cardButton.onClick.AddListener(() =>
                {
                    ShowFullImage(cardFrontImages[index]);  // ��� ������ ���� ���� ��� �����
                });
            }
            else
            {
                cardImage.sprite = lockedCardSprite;   // ������� ��� ������
                cardButton.interactable = false;   // ��� ����� ��� ������� �������
            }
        }
    }

    void ShowFullImage(Sprite sprite)
    {
        if (fullImagePanel != null && fullImageDisplay != null)
        {
            fullImageDisplay.sprite = sprite;
            fullImagePanel.SetActive(true);
        }
    }

    public void CloseFullImage()
    {
        if (fullImagePanel != null)
            fullImagePanel.SetActive(false);
    }

    public void ReturnToGame()
    {
        SceneManager.LoadScene("GameScene"); // ���� ����� ��� ��� ���� ������
    }
}
