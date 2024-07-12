using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    
    public int coins;

    [Header("Question packs in inventory")]
    public List<QuestionScriptable> QuestionsHave;
    
    public List<QuestionScriptable> QuestionsNotHave;

    
    [Header("GameObjects")] 
    public GameObject Menu;
    public GameObject Chest;
    public GameObject Gameplay;

    public TextMeshProUGUI A;
    public TextMeshProUGUI B;

    public TextMeshProUGUI percentA;
    public TextMeshProUGUI percentB;
    
    public List<QuestionScriptable> currentPack;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        QuestionsNotHave = new List<QuestionScriptable>();
        QuestionsHave = new List<QuestionScriptable>();
        if (YandexGame.savesData.questionsNotHave.Count == 0 && YandexGame.savesData.questionsHave.Count == 0)
        {
            // i < {Resources/SO/Questions_Count}
            for(int i = 0; i < 13; i++)
            {
                QuestionsNotHave.Add(Resources.Load<QuestionScriptable>("SO/" + "Q_"+i.ToString()));
            }
            print(QuestionsNotHave);
            YandexGame.savesData.questionsNotHave = new List<QuestionScriptable>();
            YandexGame.savesData.questionsNotHave = QuestionsNotHave;
            YandexGame.SaveProgress();
        }
        else
        {
            QuestionsHave = YandexGame.savesData.questionsHave;
            QuestionsNotHave = YandexGame.savesData.questionsNotHave;
        }
    }

    public void StartGame(string packName)
    {
        currentPack = new List<QuestionScriptable>();
        Menu.SetActive(false);
        Gameplay.SetActive(true);

        switch (packName)
        {
            // case "nature":
            //     //currentPack = basicQuestions;
            //     foreach (var que in NatureQ)
            //     {
            //         currentPack.Add(que);
            //     }
            //     break;
            //
            // default:
            //     foreach (var que in NatureQ)
            //     {
            //         currentPack.Add(que);
            //     }
            //     break;
        }
        
        NextQuestion();
    }

    public QuestionScriptable GetRandomQuestion(List<QuestionScriptable> questions)
    {
        int id = Random.Range(0, questions.Count);
        QuestionScriptable question = questions[id];
        questions.Remove(question);
        return question;
    }
    
    
    public void AnswerQuestion()
    {
        StartCoroutine(AnswerQuestionIEn());
    }
    
    public IEnumerator AnswerQuestionIEn()
    {
        percentA.gameObject.SetActive(true);
        percentB.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);
        
        percentA.gameObject.SetActive(false);
        percentB.gameObject.SetActive(false);
        
        NextQuestion();
    }

    public void NextQuestion()
    {
        if (currentPack.Count <= 1)
        {
            Gameplay.SetActive(false);
            Menu.SetActive(true);
        }
        QuestionScriptable question = GetRandomQuestion(currentPack);
        percentA.text = question.p1.ToString();
        percentB.text = question.p2.ToString();
        
        if (YandexGame.lang == "ru")
        {
            A.text = question.ru_a;
            B.text = question.ru_b;
        }
        else if (YandexGame.lang == "en")
        {
            A.text = question.en_a;
            B.text = question.en_b;
        }
        else if (YandexGame.lang == "tr")
        {
            A.text = question.tr_a;
            B.text = question.tr_b;
        }
    }

    private void OnApplicationQuit()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }

    public enum QuestionGroup
    {
        Nature,
        Food,
        Cars,
        Games
    }
}
