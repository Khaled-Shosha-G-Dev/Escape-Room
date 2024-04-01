using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class TouchButton : XRBaseInteractable
{
    [SerializeField] private int buttonNumber;
    [SerializeField] private Material hoverButtonMaterialFree;
    [SerializeField] private Material hoverButtonMaterialPressed;
    [SerializeField] private TextMeshProUGUI text;
    private  Renderer renderer;

    protected override void Awake()
    {
        base.Awake();
        renderer = GetComponent<Renderer>();
    }
    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
        renderer.material = hoverButtonMaterialPressed;
        text.text += buttonNumber.ToString();
    }
    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);
        renderer.material = hoverButtonMaterialFree;
    }
}
