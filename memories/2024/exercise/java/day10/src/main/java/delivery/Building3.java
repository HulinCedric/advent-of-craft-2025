package delivery;

import java.util.ArrayList;
import java.util.List;

public class Building3 {
    public static int whichFloor(String instructions) {
        boolean hasElf = instructions.contains("🧝");
        List<Integer> val = new ArrayList<>();

        for (int i = 0; i < instructions.length(); i++) {
            char c = instructions.charAt(i);
            
            if (hasElf) {
                val.add(c == ')' ? 3 : -2);
            } else {
                val.add(c == '(' ? 1 : -1);
            }
        }

        int result = 0;
        for (int v : val) {
            result += v;
        }

        return result;
    }
}
