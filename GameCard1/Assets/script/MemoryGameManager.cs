using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // تأكد من تضمين هذا الاستيراد

public class MemoryGameManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform cardContainer;
    public Sprite[] cardFrontImages;
    public Sprite cardBackImage;

    public Text scoreText;
    public Text levelText;
    public Button nextLevelButton;

    private List<Button> currentCards = new List<Button>();
    private List<int> cardValues = new List<int>();
    private Button firstRevealed = null;
    private Button secondRevealed = null;

    private int score = 0;
    private int matchedPairs = 0;
    private int level = 1;

    void Start()
    {
        LoadProgress(); // تحميل المستوى والنقاط
        nextLevelButton.gameObject.SetActive(false);
        GenerateLevel();
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
        yield return new WaitForSeconds(1f);

        int val1 = cardValues[currentCards.IndexOf(card1)];
        int val2 = cardValues[currentCards.IndexOf(card2)];

        if (val1 == val2)
        {
            matchedPairs++;
            card1.interactable = false;
            card2.interactable = false;
            score += 10;
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

    void UpdateScoreText()
    {
        scoreText.text = ":ﺔﺠﻴﺘﻨﻟﺍ " + score;
    }

    void UpdateLevelText()
    {
        levelText.text = " :ﺔﻠﺣﺮﻤﻟﺍ: " + level;
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
        SaveProgress(); // حفظ النقاط والمستوى
        GenerateLevel();
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

    // 💾 حفظ التقدم
    void SaveProgress()
    {
        PlayerPrefs.SetInt("SavedScore", score);
        PlayerPrefs.SetInt("SavedLevel", level);
        PlayerPrefs.Save();
    }

    // 📥 تحميل التقدم
    void LoadProgress()
    {
        score = PlayerPrefs.GetInt("SavedScore", 0);
        level = PlayerPrefs.GetInt("SavedLevel", 1);
    }

    // زر إعادة التقدم
    public void ResetProgress()
    {
        // مسح التقدم المحفوظ
        PlayerPrefs.DeleteKey("SavedScore");
        PlayerPrefs.DeleteKey("SavedLevel");
        PlayerPrefs.Save();

        // إعادة تعيين النقاط والمستوى
        score = 0;
        level = 1;

        // تحديث النصوص
        UpdateScoreText();
        UpdateLevelText();

        // إخفاء زر الانتقال إلى المستوى التالي في حال كانت اللعبة في مرحلة سابقة
        nextLevelButton.gameObject.SetActive(false);

        // إعادة تحميل المشهد الخاص باللعبة
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

