using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Questiondata : MonoBehaviour
{
    public Question questions;
    [SerializeField] private Text _questiontext;

    // Start is called before the first frame update
    void Start()
    {
        AskQuestion();
    }

    public void AskQuestion()
    {
        if (countvalidquestion()==0)
        {
            _questiontext.text = string.Empty;
            clearquestion();
            SceneManager.LoadScene("gameplay");
            return;
        }
        var randomindex = 0;
        do
        {
            randomindex = UnityEngine.Random.RandomRange(0, questions.questionList.Count);
        } while (questions.questionList[randomindex].questioned == true);

        questions.currentQuestion = randomindex;
        questions.questionList[questions.currentQuestion].questioned = true;
        _questiontext.text = questions.questionList[questions.currentQuestion].question;

    }
    public void clearquestion()
    {
         
    }

    private int countvalidquestion()
    {
        int validquestion = 0;
        foreach(var question in questions.questionList)
        {
            print(1);
            if (question.questioned == false)
            {
                validquestion++;
                print(2);
            }
        }
        Debug.Log(message: "question left " + validquestion);
        return validquestion;

    }
   
}
