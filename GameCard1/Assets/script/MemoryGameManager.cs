using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MemoryGameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform cardContainer;
    public Sprite[] cardFrontImages;
    public Sprite cardBackImage;

    public Text scoreText;
    public Text levelText;
    public Button nextLevelButton;
    public Text countdownText;

    public GameObject fullScreenPanel;
    public Image fullScreenImage;
    public Button closeButton;

    private HashSet<int> shownCards = new HashSet<int>();
    private List<Button> currentCards = new List<Button>();
    private List<int> cardValues = new List<int>();
    private Button firstRevealed = null;
    private Button secondRevealed = null;

    private int score = 0;
    private int matchedPairs = 0;
    private int level = 1;

    void Start()
    {
        LoadProgress();
        nextLevelButton.gameObject.SetActive(false);
        StartCoroutine(CountdownAndReveal());
    }

    IEnumerator CountdownAndReveal()
    {
        countdownText.gameObject.SetActive(true);

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.gameObject.SetActive(false);
        GenerateLevel();

        for (int i = 0; i < currentCards.Count; i++)
        {
            int value = cardValues[i];
            currentCards[i].GetComponent<Image>().sprite = cardFrontImages[value];
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < currentCards.Count; i++)
        {
            currentCards[i].GetComponent<Image>().sprite = cardBackImage;
        }
    }

    void GenerateLevel()
    {
        ClearCards();
        matchedPairs = 0;

        UpdateScoreText();
        UpdateLevelText();

        int numCards = GetCardCountForLevel(level);
        cardValues.Clear();

        for (int i = 0; i < numCards / 2; i++)
        {
            cardValues.Add(i);
            cardValues.Add(i);
        }

        Shuffle(cardValues);

        for (int i = 0; i < numCards; i++)
        {
            GameObject cardObj = Instantiate(cardPrefab, cardContainer);
            Button cardButton = cardObj.GetComponent<Button>();
            int value = cardValues[i];

            cardButton.onClick.AddListener(() => OnCardClicked(cardButton, value));
            cardButton.GetComponent<Image>().sprite = cardBackImage;

            currentCards.Add(cardButton);
        }
    }

    void OnCardClicked(Button card, int value)
    {
        if (firstRevealed != null && secondRevealed != null) return;
        if (card.GetComponent<Image>().sprite != cardBackImage) return;

        card.GetComponent<Image>().sprite = cardFrontImages[value];

        if (firstRevealed == null)
        {
            firstRevealed = card;
        }
        else
        {
            secondRevealed = card;
            StartCoroutine(CheckMatch(firstRevealed, secondRevealed));
        }
    }

    IEnumerator CheckMatch(Button card1, Button card2)
    {
        yield return new WaitForSeconds(0.5f);

        int val1 = cardValues[currentCards.IndexOf(card1)];
        int val2 = cardValues[currentCards.IndexOf(card2)];

        if (val1 == val2)
        {
            matchedPairs++;
            card1.interactable = false;
            card2.interactable = false;
            score += 10;

            if (!shownCards.Contains(val1))
            {
                shownCards.Add(val1);
                SaveShownCard(val1);
                yield return StartCoroutine(ShowCardFullScreen(val1));
            }
        }
        else
        {
            card1.GetComponent<Image>().sprite = cardBackImage;
            card2.GetComponent<Image>().sprite = cardBackImage;
            score -= 2;
        }

        UpdateScoreText();
        firstRevealed = null;
        secondRevealed = null;

        if (matchedPairs == cardValues.Count / 2)
        {
            nextLevelButton.gameObject.SetActive(true);
        }
    }

    IEnumerator ShowCardFullScreen(int cardValue)
    {
        fullScreenPanel.SetActive(true);
        fullScreenImage.sprite = cardFrontImages[cardValue];

        bool waiting = true;
        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(() =>
        {
            fullScreenPanel.SetActive(false);
            waiting = false;
        });

        while (waiting)
        {
            yield return null;
        }
    }

    void SaveShownCard(int cardValue)
    {
        string key = "UnlockedCard_" + cardValue;
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }

    public bool IsCardUnlocked(int cardValue)
    {
        string key = "UnlockedCard_" + cardValue;
        return PlayerPrefs.GetInt(key, 0) == 1;
    }

    void UpdateScoreText()
    {
        scoreText.text = score + ":ﺔﺠﻴﺘﻨﻟﺍ";
    }

    void UpdateLevelText()
    {
        levelText.text = level + " :ﺔﻠﺣﺮﻤﻟﺍ";
    }

    int GetCardCountForLevel(int lvl)
    {
        return Mathf.Min(20, 4 + ((lvl - 1) / 2) * 2);
    }

    void ClearCards()
    {
        foreach (var card in currentCards)
        {
            Destroy(card.gameObject);
        }
        currentCards.Clear();
    }

    public void NextLevel()
    {
        level++;
        nextLevelButton.gameObject.SetActive(false);
        SaveProgress();
        StartCoroutine(CountdownAndReveal());
    }

    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }

    void SaveProgress()
    {
        PlayerPrefs.SetInt("SavedScore", score);
        PlayerPrefs.SetInt("SavedLevel", level);
        PlayerPrefs.Save();
    }

    void LoadProgress()
    {
        score = PlayerPrefs.GetInt("SavedScore", 0);
        level = PlayerPrefs.GetInt("SavedLevel", 1);

        // ✅ تحميل البطاقات التي سبق عرضها
        shownCards.Clear();
        for (int i = 0; i < cardFrontImages.Length; i++)
        {
            if (IsCardUnlocked(i))
            {
                shownCards.Add(i);
            }
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteKey("SavedScore");
        PlayerPrefs.DeleteKey("SavedLevel");

        for (int i = 0; i < cardFrontImages.Length; i++)
        {
            PlayerPrefs.DeleteKey("UnlockedCard_" + i);
        }

        PlayerPrefs.Save();

        score = 0;
        level = 1;

        UpdateScoreText();
        UpdateLevelText();

        nextLevelButton.gameObject.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
