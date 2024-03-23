using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// Klasa bazowa dla element�w interaktywnych
public class InteractiveElement : MonoBehaviour, INotifyPropertyChanged
{
    //Oznacza �e Gracz zadzia�a� z elementem. Np. dla p�ytki naciskowej - gracz jest na niej
    private bool _isActivated = false;

    public bool IsActivated { get => _isActivated;
        set
        {
            _isActivated = value;
            RaisePropertyChanged("IsActivated");
        } 
    }

    //Powinna zmienia� si� na null je�eli gracz aktywnie nie korzysta z aktywatora
    private GameObject _playerGameObject;

    public GameObject PlayerGameObject
    {
        get { return _playerGameObject; }
        set { _playerGameObject = value; }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    private void RaisePropertyChanged(string propertyName)
    {
        var propChange = PropertyChanged;
        if (propChange == null) return;
        propChange(this, new PropertyChangedEventArgs(propertyName));
    }
}
