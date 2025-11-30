using System.ComponentModel;
using System.Reflection;

namespace RockPaperScissorsGame
{
    public enum Choice
    {
        Rock,
        Paper,
        Scissors
    }

    public enum Winner
    {
        Player1,
        Player2,
        Draw
    }
    
    public enum Reason
    {
        [Description("same choice")]
        SameChoice,
        [Description("rock crushes scissors")]
        RockCrushesScissors,
        [Description("paper covers rock")]
        PaperCoversRock,
        [Description("scissors cuts paper")]
        ScissorsCutsPaper
    }

    public static class ReasonExtensions
    {
        public static string GetDescription(this Reason value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }
    }

    public record Result(Winner Winner, string Reason);

    public static class RockPaperScissors
    {
      
        public static Result? Play(Choice player1, Choice player2)
        {
            var outcomes = new Dictionary<(Choice, Choice), Result>
            {
                // Player 1 wins cases  
                { (Choice.Rock, Choice.Scissors), new Result(Winner.Player1, Reason.RockCrushesScissors.GetDescription()) },
                { (Choice.Paper, Choice.Rock), new Result(Winner.Player1, Reason.PaperCoversRock.GetDescription()) },
                { (Choice.Scissors, Choice.Paper), new Result(Winner.Player1, Reason.ScissorsCutsPaper.GetDescription()) },

                // Player 2 wins cases
                { (Choice.Scissors, Choice.Rock), new Result(Winner.Player2, Reason.RockCrushesScissors.GetDescription()) },
                { (Choice.Rock, Choice.Paper), new Result(Winner.Player2, Reason.PaperCoversRock.GetDescription()) },
                { (Choice.Paper, Choice.Scissors), new Result(Winner.Player2, Reason.ScissorsCutsPaper.GetDescription()) }
            };

            return outcomes.TryGetValue((player1, player2), out var result) ? result : new Result(Winner.Draw, Reason.SameChoice.GetDescription());
        }
    }
}