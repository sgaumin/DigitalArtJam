using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Highlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{

    Text txt;
    Color baseColor;
    Button btn;
    bool interactableDelay;

    void Start()
    {
        txt = GetComponentInChildren<Text>();
        baseColor = txt.color;
        btn = gameObject.GetComponent<Button>();
        interactableDelay = btn.interactable;
    }

    void Update()
    {
        if (btn.interactable != interactableDelay)
        {
            if (btn.interactable)
            {
                txt.color = baseColor * btn.colors.normalColor * btn.colors.colorMultiplier;
            }
            else
            {
               txt.color = baseColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
            }
        }
        interactableDelay = btn.interactable;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (btn.interactable)
        {
            txt.color = baseColor * btn.colors.highlightedColor * btn.colors.colorMultiplier;
        }
        else
        {
            txt.color = baseColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (btn.interactable)
        {
            //txt.color = baseColor * btn.colors.pressedColor * btn.colors.colorMultiplier;
            txt.color = baseColor;
        }
        else
        {
            //txt.color = baseColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
            txt.color = baseColor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (btn.interactable)
        {
            //txt.color = baseColor * btn.colors.highlightedColor * btn.colors.colorMultiplier;
            txt.color = baseColor;
        }
        else
        {
            //txt.color = baseColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
            txt.color = baseColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (btn.interactable)
        {
            //txt.color = baseColor * btn.colors.normalColor * btn.colors.colorMultiplier;
            txt.color = baseColor; 
        }
        else
        {
            //txt.color = baseColor * btn.colors.disabledColor * btn.colors.colorMultiplier;
            txt.color = baseColor; 
        }
    }

}

