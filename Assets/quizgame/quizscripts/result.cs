using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class result : MonoBehaviour
{
    public Question question;

    public GameObject correctsprite;
    public GameObject incorrectsprite;

    public score Score;

    public Button truebutton;
    public Button falsbutton;
    public UnityEvent onNextquestion;
    // Start is called before the first frame update
    void Start()
    {
        correctsprite.SetActive(false);
        incorrectsprite.SetActive(false);

        
    }

    public void ShowResult(bool answer)
    {
        correctsprite.SetActive(question.questionList[question.currentQuestion].isTrue == answer);
        incorrectsprite.SetActive(question.questionList[question.currentQuestion].isTrue != answer);


        if(question.questionList[question.currentQuestion].isTrue == answer)
        {
            FindObjectOfType<GAMESOUND>().winsound();
            Score.addscore();
        }
        else
        {
            FindObjectOfType<GAMESOUND>().losesound();
            Score.detuctscore();
        }

        truebutton.interactable = false;
        falsbutton.interactable = false;

        StartCoroutine(routine: ShowResult()); 

    }
    private IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(1.0f);
        correctsprite.SetActive(false);
        incorrectsprite.SetActive(false);

        truebutton.interactable = true;
        falsbutton.interactable = true;

        onNextquestion.Invoke();

    }

    
}
