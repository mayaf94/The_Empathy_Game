using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System;

public class UiController : MonoBehaviour
{
    public static UiController Instance;
    public Button DrawButton;
    public VisualElement SummeryPenal;
    public Button EndGameButton;
    public Button ContinueButton;
    [SerializeField] private TextMeshProUGUI ScoreText;
    public Label GroupAndPersonal;
    public Label BetweenRoundsPersonal;
    public Label BetweenRoundsGroup;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        var root = GetComponent<UIDocument>().rootVisualElement;
        DrawButton = root.Q<Button>("Draw_BT");
        ContinueButton = root.Q<Button>("Countinue_BT");
        SummeryPenal = root.Q<VisualElement>("SummeryPanel");
        EndGameButton = root.Q<Button>("EndGame_BT");
        GroupAndPersonal = root.Q<Label>("GropuAndPersonal_LB");
        BetweenRoundsPersonal = root.Q<Label>("BetweenRoundsPersonal_LB");
        BetweenRoundsGroup = root.Q<Label>("BetweenRoundsGroup_LB");


        DrawButton.clicked += CardSlotsManager.InstanceSlotManager.DrawCard;
        ContinueButton.clicked += OnContinueClicked;
        GameLogicScript.Instance.CurrentGameState.OnValueChanged += Showsummery;
        
        //EndGameButton.clicked += 
    }

    private void Showsummery(GameState previousValue, GameState newValue)
    {
        if (newValue == GameState.RoundEnd)
        {
            SummeryPenal.visible = true;
            GroupAndPersonal.text = SummeryScript.Instance.RoundComper();
            BetweenRoundsPersonal.text = SummeryScript.Instance.RoundBetweenComperPersonal();
            BetweenRoundsGroup.text = SummeryScript.Instance.RoundBetweenComperTeam();

        }
        else
            SummeryPenal.visible = false;
    }

    private void OnDestroy()
    {
        GameLogicScript.Instance.CurrentGameState.OnValueChanged -= Showsummery;
    }

    private void OnEndGameButtonClick()
    {

    }

    private void OnContinueClicked()
    {
        SummeryPenal.visible = false;
    }

    public void updateScore(StatsScriptableObject states)
    {
        ScoreText.text = $@"Score:
{states.PersonalPoints} personal
{states.TeamPoints} team";
    }
}
