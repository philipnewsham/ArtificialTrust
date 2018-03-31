using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChangeTextColour : MonoBehaviour
{
    private Text[] m_textChildren;
    public bool onEnable;
    private Image m_image;
    //private Color32[] m_colours = new Color32[2];
    private bool m_animationOn;
    private bool m_flashOn = true;
    private float m_colourFloat;

    void Start()
    {
        m_textChildren = GetComponentsInChildren<Text>();
        m_image = GetComponent<Image>();
        if (onEnable)
            AnimationTrigger();
    }

    public void AnimationTrigger()
    {
        m_animationOn = !m_animationOn;
        if (m_animationOn)
            StartCoroutine("FlashAnimation");
    }

    public void HoveredOver(bool isHovering)
    {
        if(isHovering)
            StopCoroutine("FlashAnimation");
        else
        {
            if (m_animationOn)
                StartCoroutine("FlashAnimation");
        }
    }

    IEnumerator FlashAnimation()
    {
        while(m_animationOn)
        {
            if (m_flashOn)
                m_colourFloat += 0.01f;
            else
                m_colourFloat -= 0.01f;

            for (int i = 0; i < m_textChildren.Length; i++)
            {
                m_textChildren[i].color = new Color(1-m_colourFloat, 1-m_colourFloat, 1-m_colourFloat, 1);
            }

            m_image.color = new Color(m_colourFloat, m_colourFloat, m_colourFloat, 1);

            if (m_colourFloat <= 0 || m_colourFloat >= 1)
                m_flashOn = !m_flashOn;

            yield return new WaitForFixedUpdate();
        }
    }
}
