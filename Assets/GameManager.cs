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
    
    [Header("Question packs")]
    public List<QuestionScriptable> basicQuestions;

    [Header("GameObjects")] 
    public GameObject Menu;
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

    public void StartGame(string packName)
    {
        currentPack = new List<QuestionScriptable>();
        Menu.SetActive(false);
        Gameplay.SetActive(true);

        switch (packName)
        {
            case "basic":
                //currentPack = basicQuestions;
                foreach (var que in basicQuestions)
                {
                    currentPack.Add(que);
                }
                break;
            
            default:
                foreach (var que in basicQuestions)
                {
                    currentPack.Add(que);
                }
                break;
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
    
}
