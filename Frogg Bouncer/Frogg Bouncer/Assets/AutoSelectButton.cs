using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSelectButton : MonoBehaviour
{
    public Button buttonToSelect;

    void OnEnable()
    {
        // Überprüfen, ob der Button vorhanden ist
        if (buttonToSelect != null)
        {
            // Button auswählen
            buttonToSelect.Select();
        }
    }
}
