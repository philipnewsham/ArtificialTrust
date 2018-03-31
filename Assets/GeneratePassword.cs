using UnityEngine;

public class GeneratePassword : MonoBehaviour
{
    private string[] letters = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

    public string GetGeneratePassword(int passwordLength)
    {
        string password = "";

        for (int i = 0; i < passwordLength; i++)
            password += letters[Random.Range(0, letters.Length)];

        return password;
    }
}
