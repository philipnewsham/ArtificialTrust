using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHoverTest : MonoBehaviour {
    private Color32[] imageColours = new Color32[2];
    private Color32[] textColours = new Color32[2];
    private Text[] m_text;
    private Image m_image;
    private bool m_allowHovering = true;
    public bool beginOn;
    void Awake()
    {
        m_text = GetComponentsInChildren<Text>();
        m_image = GetComponentInChildren<Image>();
        imageColours[0] = m_image.color;
        imageColours[1] = m_text[0].color;

        textColours[0] = imageColours[1];
        textColours[1] = imageColours[0];
        HoverOver(false);

        if (beginOn)
        {
            HoverOver(true);
            AllowHovering(false);
        }
    }


    public void AllowHovering(bool allowHovering)
    {
        m_allowHovering = allowHovering;

        if(m_allowHovering)
        {
            HoverOver(false);
        }
    }

	public void HoverOver (bool isHovering)
    {
        if (m_allowHovering)
        {
            if (isHovering)
            {
                m_image.color = imageColours[1];
                for (int i = 0; i < m_text.Length; i++)
                {
                    m_text[i].color = textColours[1];
                }
            }
            else
            {
                m_image.color = imageColours[0];
                for (int i = 0; i < m_text.Length; i++)
                {
                    m_text[i].color = textColours[0];
                }
            }
        }
	}
}
