using System;

public static class MenstrualCycleDuck
{
    // Enum for the menstrual cycle phases
    public enum MenstrualPhase
    {
        Menstruation,
        Follicular,
        Ovulation,
        Luteal,
        PMS
    }

    // State class for menstrual cycle data
    public class MenstrualCycleState
    {
        public MenstrualPhase CurrentPhase { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ExerciseAndDietTypes.ExerciseType ExerciseSuggestion { get; set; }
        public ExerciseAndDietTypes.DietType DietSuggestion { get; set; }

        // Constructor for MenstrualCycleState
        public MenstrualCycleState(
            MenstrualPhase currentPhase,
            DateTime startDate,
            DateTime endDate,
            ExerciseAndDietTypes.ExerciseType exerciseSuggestion,
            ExerciseAndDietTypes.DietType dietSuggestion)
        {
            CurrentPhase = currentPhase;
            StartDate = startDate;
            EndDate = endDate;
            ExerciseSuggestion = exerciseSuggestion;
            DietSuggestion = dietSuggestion;
        }
    }

    // Initial state for MenstrualCycle
    public static MenstrualCycleState InitialState => new MenstrualCycleState(
        MenstrualPhase.Menstruation,
        DateTime.Now,
        DateTime.Now.AddDays(5),
        ExerciseAndDietTypes.ExerciseType.LightWalking,
        ExerciseAndDietTypes.DietType.IronRichFoods
    );

    // Actions
    public static class Actions
    {
        public const string SetPhase = "SET_PHASE";
        public const string SetSuggestions = "SET_SUGGESTIONS";

        public static ReactiveReduxUtils.Action SetPhaseAction(MenstrualPhase phase) =>
            new ReactiveReduxUtils.Action(SetPhase, phase);

        public static ReactiveReduxUtils.Action SetSuggestionsAction(
            ExerciseAndDietTypes.ExerciseType exercise,
            ExerciseAndDietTypes.DietType diet) =>
            new ReactiveReduxUtils.Action(SetSuggestions, exercise, diet);
    }

    // Reducers
    public static class Reducers
    {
        public static ReactiveReduxUtils.Reducer<MenstrualCycleState> MenstrualCycleReducer = (state, action) =>
        {
            switch (action.Type)
            {
                case Actions.SetPhase:
                    return new MenstrualCycleState(
                        (MenstrualPhase)action.Payload,
                        state.StartDate,
                        state.EndDate,
                        state.ExerciseSuggestion,
                        state.DietSuggestion
                    );
                case Actions.SetSuggestions:
                    var suggestions = (object[])action.Payload;
                    return new MenstrualCycleState(
                        state.CurrentPhase,
                        state.StartDate,
                        state.EndDate,
                        (ExerciseAndDietTypes.ExerciseType)suggestions[0],
                        (ExerciseAndDietTypes.DietType)suggestions[1]
                    );
                default:
                    return state;
            }
        };
    }

    // Selectors
    public static class Selectors
    {
        public static Func<MenstrualCycleState, MenstrualPhase> GetCurrentPhase = state => state.CurrentPhase;
        public static Func<MenstrualCycleState, ExerciseAndDietTypes.ExerciseType> GetExerciseSuggestion = state => state.ExerciseSuggestion;
        public static Func<MenstrualCycleState, ExerciseAndDietTypes.DietType> GetDietSuggestion = state => state.DietSuggestion;
    }

    // Utility method to get current phase
    public static MenstrualPhase GetCurrentPhase(DateTime cycleStartDate)
    {
        var cycleLength = 28;
        var currentDay = (DateTime.Now - cycleStartDate).Days % cycleLength;

        if (currentDay <= 5) return MenstrualPhase.Menstruation;
        if (currentDay <= 14) return MenstrualPhase.Follicular;
        if (currentDay <= 20) return MenstrualPhase.Ovulation;
        if (currentDay <= 26) return MenstrualPhase.Luteal;
        return MenstrualPhase.PMS;
    }

    // In MenstrualCycleDuck.cs

    // Method to get exercise and diet suggestions based on phase
    public static (ExerciseAndDietTypes.ExerciseType, ExerciseAndDietTypes.DietType) GetRecommendations(MenstrualPhase phase)
    {
        switch (phase)
        {
            case MenstrualPhase.Menstruation:
                return (ExerciseAndDietTypes.ExerciseType.LightWalking, ExerciseAndDietTypes.DietType.IronRichFoods);
            case MenstrualPhase.Follicular:
                return (ExerciseAndDietTypes.ExerciseType.StrengthTraining, ExerciseAndDietTypes.DietType.HighProtein);
            case MenstrualPhase.Ovulation:
                return (ExerciseAndDietTypes.ExerciseType.Cardio, ExerciseAndDietTypes.DietType.AntioxidantRichFoods);
            case MenstrualPhase.Luteal:
                return (ExerciseAndDietTypes.ExerciseType.Yoga, ExerciseAndDietTypes.DietType.ComplexCarbs);
            case MenstrualPhase.PMS:
                return (ExerciseAndDietTypes.ExerciseType.Stretching, ExerciseAndDietTypes.DietType.MagnesiumRichFoods);
            default:
                return (ExerciseAndDietTypes.ExerciseType.LightWalking, ExerciseAndDietTypes.DietType.IronRichFoods);
        }
    }

}
