using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScrollingText : MonoBehaviour {
    public string textToShow;
    private string m_writtenText;
    private char[] lettersToShow;
    public Text textBox;

	public void ParseText (string message)
    {
        textToShow = message;
        lettersToShow = textToShow.ToCharArray();
        StartCoroutine("TextScroll");
	}

    int m_curLetter;
    IEnumerator TextScroll()
    {
        while(m_curLetter < lettersToShow.Length)
        {
            m_writtenText += lettersToShow[m_curLetter].ToString();
            textBox.text = m_writtenText;
            m_curLetter += 1;
            yield return new WaitForFixedUpdate();
        }
    }
}
