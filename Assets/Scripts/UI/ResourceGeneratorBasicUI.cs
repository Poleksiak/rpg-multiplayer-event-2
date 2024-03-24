using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class ResourceGeneratorBasicUI : MonoBehaviour
{
    public ResourceGenerator resourceGenerator;
    public TextMeshProUGUI resourceAmountText;
    public TextMeshProUGUI generationStateText;

    private void Awake()
    {
        // Pod��czamy si� do zdarzenia zmiany ilo�ci zasob�w w strukturze - tak by UI by�o automatycznie aktualizowane przy zmienia stanu zasob�w
        resourceGenerator.PropertyChanged += OnPropertyChanged;
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "ResourceAmount")
        {
            resourceAmountText.text = resourceGenerator.ResourceAmount.ToString();
        }
        else if (e.PropertyName == "IsGenerating")
        {
            if (resourceGenerator.IsGenerating)
            {
                generationStateText.text = "Working";
            }
            else
            {
                generationStateText.text = "Idle";
            }
        }
    }


}
