using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NumberPad : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI typedPassword;
    [SerializeField] private TextMeshProUGUI realPassword;
    [SerializeField] private Renderer activateCard;
    [SerializeField] private Material activeCard;
    // Update is called once per frame
    void Update()
    {
        Password();
    }
    private void Password()
    {
        if (typedPassword.text.Length == 4)
        {
            if (typedPassword.text == realPassword.text.Replace(" ", ""))
            { //enable card
                typedPassword.color = Color.green;
                activateCard.material = activeCard;
            }
            else if (typedPassword.text != realPassword.text)
                typedPassword.text = "";
        }
        else if(typedPassword.text.Length>4)
            typedPassword.text = "";
    }
}
