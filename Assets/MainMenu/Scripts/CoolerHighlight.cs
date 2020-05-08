using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class CoolerHighlight : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TextMeshProUGUI text;
    public float pressedAnimationSpeed = 0.5f;
    private Color defaultColor;
    AudioSource audioSource;
    public ButtonClickAudioSet audioSet;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        defaultColor = text.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.red;
        setAndPlayAudio(audioSet.highlighted);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = defaultColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(pressedCoroutine(pressedAnimationSpeed));
        setAndPlayAudio(audioSet.clicked);
    }

    public void setAndPlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    IEnumerator pressedCoroutine(float duration)
    {
        text.color = Color.cyan;
        yield return new WaitForSeconds(duration);
        text.color = defaultColor;
    }
}
