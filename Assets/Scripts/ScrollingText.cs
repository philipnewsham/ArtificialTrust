using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour
{
    private string m_writtenText;
    private char[] lettersToShow;
    public Text textBox;

	public void ParseText (string message)
    {
        lettersToShow = message.ToCharArray();
        StartCoroutine("TextScroll");
	}
    
    IEnumerator TextScroll()
    {
        int currentLetter = 0;
        while(currentLetter < lettersToShow.Length)
        {
            m_writtenText += lettersToShow[currentLetter].ToString();
            textBox.text = m_writtenText;
            currentLetter += 1;
            yield return null;
        }
    }
}
