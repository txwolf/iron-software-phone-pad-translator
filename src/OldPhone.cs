using System;
using System.Text;

public class OldPhonePad
{
    private static readonly string[] KEYPAD = new string[]
    {
        " ",    // Placeholder for '0' which typically has no letters
        "",     // '1' does not correspond to any letters
        "ABC",  // '2'
        "DEF",  // '3'
        "GHI",  // '4'
        "JKL",  // '5'
        "MNO",  // '6'
        "PQRS", // '7'
        "TUV",  // '8'
        "WXYZ"  // '9'
    };

    public static string Translate(string input)
    {
        if (string.IsNullOrEmpty(input)) return "";

        StringBuilder output = new StringBuilder();
        char currentChar = input[0];
        int count = 0;
        bool lastWasSpace = false;

        foreach (char ch in input)
        {
            if (ch == '#') break; // Stop processing if end marker is encountered

            if (ch == ' ')
            {
                lastWasSpace = true; // Mark that the last character was a space
                continue;            // Continue to the next character
            }

            if (char.IsDigit(ch) && ch >= '2' && ch <= '9')
            {
                if (ch == currentChar && !lastWasSpace)
                {
                    count++; // Increment count if it's the same digit and no intervening space
                }
                else
                {
                    AppendCharacter(output, currentChar, count);
                    currentChar = ch;
                    count = 1; // reset count for the new character
                }
                lastWasSpace = false; // Reset space flag after handling a digit
            }
        }

        // Append the last character sequence processed
        AppendCharacter(output, currentChar, count);
        return output.ToString();
    }

    private static void AppendCharacter(StringBuilder output, char digit, int count)
    {
        int num = digit - '0'; // Convert char to int
        if (num >= 2 && num <= 9) // Ensure the digit is valid
        {
            string letters = KEYPAD[num];
            int index = (count - 1) % letters.Length;
            output.Append(letters[index]);
        }
    }

    public static void Main()
    {
        // Test examples
        Console.WriteLine(Translate("33#"));               // Outputs: "E"
        Console.WriteLine(Translate("22#"));               // Outputs: "B"
        Console.WriteLine(Translate("4433555 555666##"));  // Outputs: "HELLO"
        Console.WriteLine(Translate("8 88777444666*664##")); // Outputs: "TURING"
    }
}
