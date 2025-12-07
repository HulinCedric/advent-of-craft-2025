using System.Linq;

namespace Delivery
{
    /// <summary>
    /// Building floor calculator for delivery instructions.
    /// </summary>
    public static class Building4
    {
        private const int ElfUp = 3;
        private const int ElfDown = -2;
        private const int StandardUp = 1;
        private const int StandardDown = -1;
        private const string ElfIndicator = "🧝";

        public static int WhichFloor(string instructions)
        {
            bool isElfMode = instructions.Contains(ElfIndicator);
            
            return instructions
                .Where(ch => ch == '(' || ch == ')')
                .Sum(ch => CalculateFloorMovement(ch, isElfMode));
        }
        
        private static int CalculateFloorMovement(char direction, bool isElfMode)
        {
            return isElfMode
                ? (direction == ')' ? ElfUp : ElfDown)
                : (direction == '(' ? StandardUp : StandardDown);
        }
    }
}