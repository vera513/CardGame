using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StudioManager : MonoBehaviour
{
    public Transform cardGrid;                // «·⁄‰’— Content œ«Œ· Scroll View
    public GameObject cardPrefab;             // Prefab ÌÕ ÊÌ ⁄·Ï Image ›ﬁÿ
    public Sprite[] cardFrontImages;          // ’Ê— «·»ÿ«ﬁ«  «·√’·Ì… (»Õ”» «· — Ì»)
    public Sprite lockedCardSprite;           // ’Ê—… —„«œÌ… √Ê €«„÷… ··»ÿ«ﬁ«  €Ì— «·„› ÊÕ…

    public GameObject fullImagePanel;         // Panel ·⁄—÷ «·»ÿ«ﬁ… »«·ÕÃ„ «·ﬂ«„·
    public Image fullImageDisplay;            // ’Ê—… «·»ÿ«ﬁ… œ«Œ· Panel

    void Start()
    {
        LoadAllCards();
        if (fullImagePanel != null)
            fullImagePanel.SetActive(false); // ≈Œ›«¡ ‰«›–… «·⁄—÷ »«·»œ«Ì…
    }

    void LoadAllCards()
    {
        for (int i = 0; i < cardFrontImages.Length; i++)
        {
            int index = i; // „Â„ · Ã‰» „‘«ﬂ· «··Ê»

            GameObject cardObj = Instantiate(cardPrefab, cardGrid);
            Image cardImage = cardObj.GetComponent<Image>();

            // ≈÷«›… Button  ·ﬁ«∆Ì« ≈–« ﬂ«‰ €Ì— „ÊÃÊœ
            Button cardButton = cardObj.GetComponent<Button>();
            if (cardButton == null)
            {
                cardButton = cardObj.AddComponent<Button>();  // ≈÷«›… Button ≈·Ï «·»ÿ«ﬁ…
            }

            //  ÕœÌœ «·’Ê—… «·„‰«”»… ··»ÿ«ﬁ…
            if (PlayerPrefs.GetInt("UnlockedCard_" + i, 0) == 1)
            {
                cardImage.sprite = cardFrontImages[i]; // «·»ÿ«ﬁ… „› ÊÕ…
                cardButton.onClick.AddListener(() =>
                {
                    ShowFullImage(cardFrontImages[index]);  // ⁄—÷ «·’Ê—… »ÕÃ„ ﬂ»Ì— ⁄‰œ «·÷€ÿ
                });
            }
            else
            {
                cardImage.sprite = lockedCardSprite;   // «·»ÿ«ﬁ… €Ì— „› ÊÕ…
                cardButton.interactable = false;   // „‰⁄ «·÷€ÿ ⁄·Ï «·»ÿ«ﬁ… «·„€·ﬁ…
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
        SceneManager.LoadScene("GameScene"); // €Ì¯— «·«”„ Õ”» «”„ „‘Âœ «··⁄»…
    }
}
