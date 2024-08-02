using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    CardManager _cardManager;
    public WordTrigger[] WordTrigger = new WordTrigger[3];

    public Button TextButton;       //텍스트 넘기기용 버튼
    public Button PauseButton;      //일시정지 버튼
    public Button RegameButton;     //재시작 버튼
    public Button SettingButton;    //설정 버튼
    public Button ReturnButton;     //돌아가기 버튼
    public Button SelectedCardsButton;      //최종카드 확인 후 진행버튼
    public Button SelectedWordsButton;      //최종 제시어 확정버튼
    public Button GuessButton;      //제외카드 배치완료 버튼

    public GameObject JobCardsButtons;      //모든 직업카드
    public GameObject ToolCardsButtons;     //모든 도구카드
    public GameObject PurposeCardsButtons; //모든 동기카드

    public Button[] JobButtons = new Button[9];     //직업버튼
    public Button[] ToolButtons = new Button[9];    //도구버튼
    public Button[] PurposeButtons = new Button[9]; //동기버튼

    public int JobNumber;       //직업카드 인덱스
    public int ToolNumber;      //도구카드 인덱스
    public int PurposeNumber;   //동기카드 인덱스

    public GameObject TextPanel;

    private int _finalJobIndex;     //최종 직업카드 인덱스
    private int _finalToolIndex;    //최종 도구카드 인덱스
    private int _finalPurposeIndex; //최종 동기카드 인덱스

    public Button CardSelectedButton;       //카드 선택완료 버튼

    public Button ConfirmButton;    //재시작 확인버튼
    public Button CancelButton;     //재시작 취소버튼

    public GameObject PausePanel;   //일시정지 판넬
    public GameObject RegamePanel;  //재시작 판넬

    //텍스트
    public GameObject Text1;
    public GameObject Text2;
    public GameObject Text3;

    //직업, 도구, 동기 버튼 선택확인
    [SerializeField] private bool _jobCardSelected;
    [SerializeField] private bool _toolCardSelected;
    [SerializeField] private bool _purposeCardSelected;    

    void Awake()
    {
        _cardManager = GameObject.Find("CardManager").GetComponent<CardManager>();

        TextButton.onClick.AddListener(OnTextButtonClicked);
        PauseButton.onClick.AddListener(OnPauseButtonClicked);
        RegameButton.onClick.AddListener(OnRegameButtonClicked);
        ReturnButton.onClick.AddListener(OnReturnButtonClicked);
        SelectedCardsButton.onClick.AddListener(OnSelectedCardsButtonClicked);
        SelectedWordsButton.onClick.AddListener(OnSelectedWordsButtonClicked);
        GuessButton.onClick.AddListener(OnGuessButtonClicked);

        CardSelectedButton.onClick.AddListener(OnCardSelectedButtonClicked);

        ConfirmButton.onClick.AddListener(OnConfirmButtonClicked);
        CancelButton.onClick.AddListener(OnCancelButtonClicked);        

        //직업카드 버튼선택
        for(int i = 0; i < JobButtons.Length; i++)
        {
            int number = i;
            JobButtons[i].onClick.AddListener(() => OnJobButtonClicked(number));
        }

        //도구카드 버튼선택
        for(int i = 0; i < ToolButtons.Length; i++)
        {
            int number = i;
            ToolButtons[i].onClick.AddListener(() => OnToolButtonClicked(number));
        }

        //동기카드 버튼선택
        for(int i = 0; i < PurposeButtons.Length; i++)
        {
            int number = i;
            PurposeButtons[i].onClick.AddListener(() => OnPurposeButtonClicked(number));
        }
    }
    
    void Update()
    {
        if(_jobCardSelected && _toolCardSelected && _purposeCardSelected)
        {
            CardSelectedButton.gameObject.SetActive(true);
        }        
    }

    void OnJobButtonClicked(int number)
    {
        for(int i = 0; i < JobButtons.Length; i++)
        {
            if (JobButtons[i].transform.GetChild(0).gameObject.activeSelf)
            {
                JobButtons[i].transform.GetChild(0).gameObject.SetActive(false);
                JobButtons[number].transform.GetChild(0).gameObject.SetActive(true);

                _jobCardSelected = true;
                JobNumber = number;
            }
            else
            {
                JobButtons[number].transform.GetChild(0).gameObject.SetActive(true);

                _jobCardSelected = true;
                JobNumber = number;
            }
        }
    }

    void OnToolButtonClicked(int number)
    {
        for(int i = 0; i < ToolButtons.Length; i++)
        {
            if (ToolButtons[i].transform.GetChild(0).gameObject.activeSelf)
            {
                ToolButtons[i].transform.GetChild(0).gameObject.SetActive(false);
                ToolButtons[number].transform.GetChild(0).gameObject.SetActive(true);

                _toolCardSelected = true;
                ToolNumber = number;
            }
            else
            {
                ToolButtons[number].transform.GetChild(0).gameObject.SetActive(true);

                _toolCardSelected = true;
                ToolNumber = number;
            }
        }
    }

    void OnPurposeButtonClicked(int number)
    {
        for(int i = 0; i < PurposeButtons.Length; i++)
        {
            if (PurposeButtons[i].transform.GetChild(0).gameObject.activeSelf)
            {
                PurposeButtons[i].transform.GetChild(0).gameObject.SetActive(false);
                PurposeButtons[number].transform.GetChild(0).gameObject.SetActive(true);

                _purposeCardSelected = true;
                PurposeNumber = number;
            }
            else
            {
                PurposeButtons[number].transform.GetChild(0).gameObject.SetActive(true);

                _purposeCardSelected = true;
                PurposeNumber = number;
            }
        }
    }

    void OnCardSelectedButtonClicked()
    {
        _jobCardSelected = false;
        _toolCardSelected = false;
        _purposeCardSelected = false;

        CardSelectedButton.gameObject.SetActive(false);

        _finalJobIndex = JobNumber;
        _finalToolIndex = ToolNumber;
        _finalPurposeIndex = PurposeNumber;

        GameManager.instance.FinalJobCard = _cardManager.JobCards[_finalJobIndex];
        GameManager.instance.FinalToolCard = _cardManager.ToolCards[_finalToolIndex];
        GameManager.instance.FinalPurposeCard = _cardManager.PurposeCards[_finalPurposeIndex];

        for(int i = 0; i < 9; i++)
        {
            JobButtons[i].gameObject.SetActive(false);
            ToolButtons[i].gameObject.SetActive(false);
            PurposeButtons[i].gameObject.SetActive(false);
        }

        _cardManager.SelectedJobCardImage.SetActive(true);
        _cardManager.SelectedToolCardImage.SetActive(true);
        _cardManager.SelectedPurposeCardImage.SetActive(true);

        _cardManager.SelectedJobCardImage.GetComponent<MeshRenderer>().material = GameManager.instance.FinalJobCard.transform.GetChild(0).GetComponent<MeshRenderer>().material;
        _cardManager.SelectedToolCardImage.GetComponent<MeshRenderer>().material = GameManager.instance.FinalToolCard.transform.GetChild(0).GetComponent<MeshRenderer>().material;
        _cardManager.SelectedPurposeCardImage.GetComponent<MeshRenderer>().material = GameManager.instance.FinalPurposeCard.transform.GetChild(0).GetComponent<MeshRenderer>().material;
    }

    void OnSelectedCardsButtonClicked()
    {
        if(_cardManager.SelectedJobCardImage.activeSelf && _cardManager.SelectedToolCardImage.activeSelf && _cardManager.SelectedPurposeCardImage.activeSelf)
        {
            _cardManager.SelectedJobCardImage.SetActive(false);
            _cardManager.SelectedToolCardImage.SetActive(false);
            _cardManager.SelectedPurposeCardImage.SetActive(false);

            TextPanel.SetActive(true);
        }
    }

    void OnSelectedWordsButtonClicked()
    {
        if (GameManager.instance.PlaceWords)
        {
            GameManager.instance.PlaceWords = false;
            GameManager.instance.FirstRoundStart = true;

            GameManager.instance.FirstAdjectiveNumbers = 0;
            GameManager.instance.FirstNounNumbers = 0;

            for (int i = 0; i < WordTrigger.Length; i++)
            {
                WordTrigger[i].PlacedAdjectiveWords[0] = WordTrigger[i].TriggeredAdjective;
                WordTrigger[i].PlacedNounWords[0] = WordTrigger[i].TriggeredNoun;

                WordTrigger[i].TriggeredAdjective.transform.position = WordTrigger[i].AdjectivePositions[0];
                WordTrigger[i].TriggeredNoun.transform.position = WordTrigger[i].NounPositions[0];

                WordTrigger[i].TriggeredAdjective.transform.gameObject.tag = "PlacedWord";
                WordTrigger[i].TriggeredNoun.transform.gameObject.tag = "PlacedWord";

                Destroy(WordTrigger[i].TriggeredAdjective.GetComponent<DragAndDrop>());
                Destroy(WordTrigger[i].TriggeredNoun.GetComponent<DragAndDrop>());

                for (int j = 0; j < 6; j++)
                {
                    if (_cardManager.FirstAdjectiveCards[j] == WordTrigger[i].TriggeredAdjective)
                    {
                        _cardManager.FirstAdjectiveCards[j] = null;
                    }

                    if (_cardManager.FirstNounCards[j] == WordTrigger[i].TriggeredNoun)
                    {
                        _cardManager.FirstNounCards[j] = null;
                    }
                }
            }

            ArrangeWords();
        }

        if (GameManager.instance.FirstGuessComplete)
        {
            GameManager.instance.FirstGuessComplete = false;
            GameManager.instance.SecondWordNumber = 0;

            GameManager.instance.SecondRoundStart = true;

            for (int i = 0; i < WordTrigger.Length; i++)
            {                
                if (WordTrigger[i].SecondTriggeredAdjective != null)
                {
                    WordTrigger[i].PlacedAdjectiveWords[1] = WordTrigger[i].SecondTriggeredAdjective;
                    WordTrigger[i].SecondTriggeredAdjective.transform.position = WordTrigger[i].AdjectivePositions[1];

                    WordTrigger[i].SecondTriggeredAdjective.transform.gameObject.tag = "PlacedWord";
                    Destroy(WordTrigger[i].SecondTriggeredAdjective.GetComponent<DragAndDrop>());
                }
                else if (WordTrigger[i].SecondTriggeredNoun != null)
                {
                    WordTrigger[i].PlacedNounWords[1] = WordTrigger[i].SecondTriggeredNoun;
                    WordTrigger[i].SecondTriggeredNoun.transform.position = WordTrigger[i].NounPositions[1];

                    WordTrigger[i].SecondTriggeredNoun.transform.gameObject.tag = "PlacedWord";
                    Destroy(WordTrigger[i].SecondTriggeredNoun.GetComponent<DragAndDrop>());
                }

                for(int j = 0; j < 6; j++)
                {
                    if (_cardManager.FirstAdjectiveCards[j] == WordTrigger[i].SecondTriggeredAdjective)
                    {
                        _cardManager.FirstAdjectiveCards[j] = null;
                    }

                    if (_cardManager.FirstNounCards[j] == WordTrigger[i].SecondTriggeredNoun)
                    {
                        _cardManager.FirstNounCards[j] = null;
                    }
                }
            }

            ArrangeWords();
        }

        if(GameManager.instance.SecondGuessComplete)
        {
            GameManager.instance.SecondGuessComplete = false;
            GameManager.instance.LastWordNumber = 0;

            GameManager.instance.LastRoundStart = true;

            for(int i = 0; i < WordTrigger.Length; i++)
            {                
                if (WordTrigger[i].SecondTriggeredAdjective != null)
                {
                    WordTrigger[i].SecondTriggeredAdjective.transform.position = WordTrigger[i].AdjectivePositions[1];
                }
                
                if (WordTrigger[i].SecondTriggeredNoun != null)
                {
                    WordTrigger[i].SecondTriggeredNoun.transform.position = WordTrigger[i].NounPositions[1];
                }
            }

            ArrangeWords();
        }
    }

    void ArrangeWords()
    {
        for (int i = 0; i < 6; i++)
        {
            if (_cardManager.FirstAdjectiveCards[i] == null)
            {
                for (int j = i; j < 6; j++)
                {
                    if (_cardManager.FirstAdjectiveCards[j] != null)
                    {
                        _cardManager.FirstAdjectiveCards[i] = _cardManager.FirstAdjectiveCards[j];
                        _cardManager.FirstAdjectiveCards[j].transform.position = _cardManager.FirstAdjectivePositions[i];
                        _cardManager.FirstAdjectiveCards[j] = null;
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < 6; i++)
        {
            if (_cardManager.FirstNounCards[i] == null)
            {
                for (int j = i; j < 6; j++)
                {
                    if (_cardManager.FirstNounCards[j] != null)
                    {
                        _cardManager.FirstNounCards[i] = _cardManager.FirstNounCards[j];
                        _cardManager.FirstNounCards[j].transform.position = _cardManager.FirstNounPositions[i];
                        _cardManager.FirstNounCards[j] = null;
                        break;
                    }
                }
            }
        }
    }

    void OnGuessButtonClicked()
    {        
            int _index;

            for (int i = 0; i < 6; i++)
            {
                GuessButton.gameObject.SetActive(false);
                GameManager.instance.GuessedCards[i] = _cardManager.ExemptionTokens[i].GetComponent<ExemptionTrigger>().TriggeredCard;
            }

            for (_index = 0; _index < GameManager.instance.GuessedCards.Length; _index++)
            {
                if (GameManager.instance.GuessedCards[_index] == GameManager.instance.FinalJobCard ||
                    GameManager.instance.GuessedCards[_index] == GameManager.instance.FinalToolCard ||
                    GameManager.instance.GuessedCards[_index] == GameManager.instance.FinalPurposeCard)
                {
                    GameManager.instance.GuessCount++;

                    for (int j = 0; j < 6; j++)
                    {
                        _cardManager.ExemptionTokens[j].transform.position = _cardManager.FirstExemptionTokenPositions[j];
                    }
                    break;
                }
                else if (GameManager.instance.GuessedCards[_index] != GameManager.instance.FinalJobCard &&
                    GameManager.instance.GuessedCards[_index] != GameManager.instance.FinalToolCard &&
                    GameManager.instance.GuessedCards[_index] != GameManager.instance.FinalPurposeCard)
                {
                    continue;
                }
            }

        if (_index == 6)
        {
            if (GameManager.instance.FirstRoundStart)
            {
                GameManager.instance.FirstGuessComplete = true;
            }
            else if(GameManager.instance.SecondRoundStart)
            {
                GameManager.instance.SecondGuessComplete = true;
            }
            
            for (int i = 0; i < GameManager.instance.GuessedCards.Length; i++)
            {
                GameManager.instance.GuessedCards[i].transform.rotation = Quaternion.Euler(90, 0, 0);
            }

            for (int i = 0; i < _cardManager.ExemptionTokens.Length; i++)
            {
                _cardManager.ExemptionTokens[i].transform.position = _cardManager.FirstExemptionTokenPositions[i];
            }

            _cardManager.SetWords();
        }        
    }

    void OnTextButtonClicked()
    {
        if(Text1.activeSelf && !Text2.activeSelf)
        {
            Text1.SetActive(false);
            Text2.SetActive(true);
        }
        else if(!Text1.activeSelf && Text2.activeSelf)
        {            
            Text2.SetActive(false);
            Text3.SetActive(true);
            TextPanel.SetActive(false);

            JobCardsButtons.SetActive(true);
            ToolCardsButtons.SetActive(true);
            PurposeCardsButtons.SetActive(true);
        }
        else if(Text3.activeSelf)
        {
            Text3.SetActive(false);
            TextPanel.SetActive(false);

            _cardManager.TurnAllCards();
            GameManager.instance.PlaceWords = true;
        }
    }

    void OnPauseButtonClicked()
    {
        PausePanel.SetActive(true);
    }

    void OnRegameButtonClicked()
    {
        RegamePanel.SetActive(true);
    }

    void OnConfirmButtonClicked()
    {
        SceneManager.LoadScene("MainScene");
    }

    void OnCancelButtonClicked()
    {
        RegamePanel.SetActive(false);
    }

    void OnReturnButtonClicked()
    {
        PausePanel.SetActive(false);
    }
}
