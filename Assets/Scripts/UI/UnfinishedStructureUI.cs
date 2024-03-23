using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class UnfinishedStructureUI : MonoBehaviour
{
    public UnfinishedStructure unfinishedStructure;
    public TextMeshProUGUI resourceAmountText;

    private void Awake()
    {
        // Pod��czamy si� do zdarzenia zmiany ilo�ci zasob�w w strukturze - tak by UI by�o automatycznie aktualizowane przy zmienia stanu
        unfinishedStructure.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "ResourceAmount")
        {
            resourceAmountText.text = (unfinishedStructure.neededResourceAmount - unfinishedStructure.ResourceAmount).ToString();
        }
    }
}
