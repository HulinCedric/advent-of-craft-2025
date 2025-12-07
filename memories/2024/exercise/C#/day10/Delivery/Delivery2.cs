namespace Delivery
{
    /// <summary>
    /// Building floor calculator for delivery instructions.
    /// </summary>
    public static class Building2
    {
        private const int ElfUpwardMovement = 3;
        private const int ElfDownwardMovement = -2;
        private const int StandardUpwardMovement = 1;
        private const int StandardDownwardMovement = -1;
        private const int SpecialUpwardMovement = 42;
        private const int SpecialDownwardMovement = -2;
        private const string ElfIndicator = "🧝";

        public static int WhichFloor(string instructions)
        {
            int currentFloor = 0;
            
            for (int position = 0; position < instructions.Length; position++)
            {
                char direction = instructions[position];
                currentFloor += CalculateFloorChange(instructions, direction);
            }
            
            return currentFloor;
        }
        
        private static int CalculateFloorChange(string instructions, char direction)
        {
            if (instructions.Contains(ElfIndicator))
            {
                return direction == ')' ? ElfUpwardMovement : ElfDownwardMovement;
            }
            else if (!instructions.Contains(ElfIndicator))
            {
                return direction == '(' ? StandardUpwardMovement : StandardDownwardMovement;
            }
            else
            {
                return direction == '(' ? SpecialUpwardMovement : SpecialDownwardMovement;
            }
        }
    }
}