namespace Delivery
{
    public static class Building3
    {
        public static int WhichFloor(string instructions)
        {
            bool hasElf = instructions.Contains("🧝");
            List<int> val = [];
            
            for (int i = 0; i < instructions.Length; i++)
            {
                var c = instructions[i];
                
                if (hasElf)
                {
                    val.Add(c == ')' ? 3 : -2);
                }
                else
                {
                    val.Add(c == '(' ? 1 : -1);
                }
            }
            
            int result = 0;
            foreach (var v in val)
            {
                result += v;
            }
            return result;
        }
    }
}