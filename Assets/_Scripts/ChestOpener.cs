using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    [SerializeField] private Transform lootLayout;
    [SerializeField] private GameObject CardPrefab;

    public List<QuestionScriptable> questionsToUnlock;
    
    public void StartOpen(Chest.ChestType type)
    {
        
        questionsToUnlock = new List<QuestionScriptable>();
        switch (type)
        {
            case Chest.ChestType.Small:
                //int i = Random.Range(3, 6)
                for (int i = Random.Range(3, 6); i > 0; i--)
                {
                    questionsToUnlock.Add(GetRandomQuestionToUnlock());
                }
                break;
        }
    }

    public QuestionScriptable GetRandomQuestionToUnlock()
    {
        int index;
        index = Random.Range(0, GameManager.Instance.QuestionsNotHave.Count);
        List<QuestionScriptable> questionsToUnlock = GameManager.Instance.QuestionsNotHave[index];

        int indexQ = Random.Range(0, questionsToUnlock.Count);
        QuestionScriptable questionToReturn = questionsToUnlock[indexQ];
        questionsToUnlock.Remove(questionToReturn);

        return questionToReturn;
    }
}
