using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MenstrualCycleUIController : MonoBehaviour
{
    // Reference to the Text elements in the UI
    public TextMeshProUGUI phaseText;
    public TextMeshProUGUI startDateText;
    public TextMeshProUGUI endDateText;
    public TextMeshProUGUI exerciseText;
    public TextMeshProUGUI dietText;

    // Reference to the Buttons
    public Button setCycleStartDateButton;
    public Button nextPhaseButton;

    MenstrualCycleDuck.MenstrualCycleState currentState;

    void Start()
    {
        // Subscribe button click events to methods
        setCycleStartDateButton.onClick.AddListener(OnSetCycleStartDate);
        nextPhaseButton.onClick.AddListener(OnNextPhase);

        // Initialize the UI with the current state
        currentState = MenstrualCycleDuck.InitialState;
        UpdateUI(currentState);
    }

    // Method to update the UI based on the state
    void UpdateUI(MenstrualCycleDuck.MenstrualCycleState state)
    {
        // Update phase, dates, exercise, and diet based on state
        phaseText.text = $"Current Phase: {state.CurrentPhase}";
        startDateText.text = $"Start Date: {state.StartDate.ToShortDateString()}";
        endDateText.text = $"End Date: {state.EndDate.ToShortDateString()}";
        exerciseText.text = $"Exercise Suggestion: {state.ExerciseSuggestion}";
        dietText.text = $"Diet Suggestion: {state.DietSuggestion}";
    }

    // Method to set the cycle start date (simulated for now)
    void OnSetCycleStartDate()
    {
        // Set a new start date and update state
        var newStartDate = DateTime.Now; // Here we could prompt the user to pick a date
        currentState = new MenstrualCycleDuck.MenstrualCycleState(
            currentPhase: MenstrualCycleDuck.GetCurrentPhase(newStartDate),
            startDate: newStartDate,
            endDate: newStartDate.AddDays(28),
            exerciseSuggestion: ExerciseAndDietTypes.ExerciseType.Yoga,
            dietSuggestion: ExerciseAndDietTypes.DietType.IronRichFoods
        );
        UpdateUI(currentState);
    }

    // Method to cycle to the next phase (example implementation)
    void OnNextPhase()
    {
        // Update the phase to the next one based on predefined order
        currentState = MenstrualCycleDuck.Reducers.MenstrualCycleReducer(currentState, MenstrualCycleDuck.Actions.SetPhaseAction(
            (MenstrualCycleDuck.MenstrualPhase)(((int)currentState.CurrentPhase + 1) % Enum.GetValues(typeof(MenstrualCycleDuck.MenstrualPhase)).Length)
        ));

        UpdateUI(currentState);
    }
}
