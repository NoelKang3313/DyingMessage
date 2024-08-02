using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject FinalJobCard;         //최종 직업카드
    public GameObject FinalToolCard;        //최종 도구카드
    public GameObject FinalPurposeCard;     //최종 동기카드

    public bool PlaceWords;             //초기 단어카드 배치
    public int FirstAdjectiveNumbers;   //초기 배치한 형용사카드 숫자
    public int FirstNounNumbers;        //초기 배치한 명사카드 숫자

    public int SecondWordNumber;        //Second Round 단어카드 숫자 카운트
    public int LastWordNumber;          //Last Round 단어카드 숫자 카운트

    public int CardNumbers;        //카드 숫자(제외토큰와 트러거된 카드)
    public GameObject[] GuessedCards = new GameObject[6];       //제외할 카드 오브젝트

    public int GuessCount = 0;      //추리실패 카운트 변수

    public bool FirstRoundStart;
    public bool SecondRoundStart;
    public bool LastRoundStart;

    public bool FirstGuessComplete;     //첫번째 추리성공
    public bool SecondGuessComplete;    //두번째 추리성공

    UIManager _UIManager;

    public static GameManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        _UIManager = GameObject.Find("UIManager").GetComponent<UIManager>();        
    }

    void Update()
    {
        CountWordCards();
        CountExemptionCards();
    }

    void CountWordCards()
    {
        if (FirstAdjectiveNumbers == 3 && FirstNounNumbers == 3)
        {
            for(int i = 0; i < _UIManager.WordTrigger.Length; i++)
            {
                if (_UIManager.WordTrigger[i].TriggeredAdjective != null)                    
                {
                    _UIManager.SelectedWordsButton.gameObject.SetActive(false);
                }

                if (_UIManager.WordTrigger[i].TriggeredNoun != null)
                {
                    _UIManager.SelectedWordsButton.gameObject.SetActive(false);
                }
            }

            _UIManager.SelectedWordsButton.gameObject.SetActive(true);
        }
        else if (FirstAdjectiveNumbers != 3 || FirstNounNumbers != 3)
        {
            _UIManager.SelectedWordsButton.gameObject.SetActive(false);
        }

        if(FirstGuessComplete && SecondWordNumber == 1)
        {
            _UIManager.SelectedWordsButton.gameObject.SetActive(true);
        }
        else if(FirstGuessComplete && SecondWordNumber != 1)
        {
            _UIManager.SelectedWordsButton.gameObject.SetActive(false);
        }

        if(SecondGuessComplete && LastWordNumber == 1)
        {
            for(int i = 0; i < _UIManager.WordTrigger.Length; i++)
            {
                if (_UIManager.WordTrigger[i].TriggeredAdjective != null)
                {
                    _UIManager.SelectedWordsButton.gameObject.SetActive(false);
                }
                
                if (_UIManager.WordTrigger[i].TriggeredNoun != null)                    
                {
                    _UIManager.SelectedWordsButton.gameObject.SetActive(false);
                }                
            }

            _UIManager.SelectedWordsButton.gameObject.SetActive(true);
        }
        else if(SecondGuessComplete && LastWordNumber != 1)
        {            
            _UIManager.SelectedWordsButton.gameObject.SetActive(false);
        }
    }

    void CountExemptionCards()
    {
        if(CardNumbers == 6)
        {
            _UIManager.GuessButton.gameObject.SetActive(true);
        }
        else
        {
            _UIManager.GuessButton.gameObject.SetActive(false);
        }
    }    
}
