using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LightButton : MonoBehaviour
{
    public int lightID;
    private GameObject m_ai;
    private LightController m_lightController;
    private bool m_isOn;
    public Sprite[] sprites;
    private Image m_image;

    void Start()
    {
        m_image = gameObject.GetComponent<Image>();
    }

    public void Power()
    {
        m_isOn = !m_isOn;
        m_image.sprite = sprites[m_isOn == true ? 0 : 1];
    }
}
