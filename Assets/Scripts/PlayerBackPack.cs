using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerBackPack : MonoBehaviour, INotifyPropertyChanged
{
    // Zmienne z notifyPropertyChanged
    private int resourceAmount;
    public int ResourceAmount
    {
        get { return resourceAmount; }
        set
        {
            resourceAmount = value;
            RaisePropertyChanged("ResourceAmount");
        }
    }

    // Zas�b w plecaku
    public GameResource resource;
    public int maxResourceAmount = 10;
    // Kolekcja zasob�w w plecaku, grid do po�o�enia zasob�w w widoku gry
    public GridGameObjectCollection gridCollection;


    public event PropertyChangedEventHandler PropertyChanged;

    private void RaisePropertyChanged(string propertyName)
    {
        var propChange = PropertyChanged;
        if (propChange == null) return;
        propChange(this, new PropertyChangedEventArgs(propertyName));
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "ResourceAmount")
        {
            //Jest przypadek �e np. ta courtyna uruchomi si� par� razy przy zmianie ResourceAmount wielokrotnym zanim zd��y si� sko�czy� - naprawi�
            StartCoroutine(UpdateBackPackGridCollection());
        }
    }

    private void Awake()
    {
        this.PropertyChanged += OnPropertyChanged;
    }

    public bool IsInTransferPosibble(GameResource resourceType, int amount)
    {
        bool resourceTypeCheck = resource == null ? true : resourceType == resource;
        return resourceTypeCheck && ResourceAmount < maxResourceAmount;
    }

    public bool IsOutTransferPosibble(GameResource resourceType, int amount)
    {
        bool resourceTypeCheck = resourceType == null ? true : resourceType == resource;
        return resourceTypeCheck && ResourceAmount > 0;
    }

    //Transfer zasob�w do plecaka
    public bool TransferIn(GameResource resourceType, int amount)
    {
        Debug.Log("Przesy� zasob�w start! pr�ba!!!");
        if (IsInTransferPosibble(resourceType, amount))
        {
            resource = resourceType;
            gridCollection.prefab = resource.prefab;
            ResourceAmount += amount;
            Debug.Log("Przesy� zasob�w uda�o si�!!!");
            return true;
        }
        else
        {
            Debug.Log("Przesy� zasob�w nie uda� si� :C");
        }
        return false;
    }
    //Transfer zasob�w z plecaka
    public bool TransferOut(GameResource resourceType, int amount)
    {
        if (IsOutTransferPosibble(resourceType, amount))
        {
            ResourceAmount -= amount;
            if(ResourceAmount == 0)
            {
                resource = null;
                gridCollection.prefab = null;
            }
            return true;
        }
        return false;
    }

    private IEnumerator UpdateBackPackGridCollection()
    {
        while (this.ResourceAmount != gridCollection.gridObjects.Count)
        {
            if (this.ResourceAmount > gridCollection.gridObjects.Count)
            {
                gridCollection.AddObjectToGrid();
            }
            else
            {
                gridCollection.RemoveObjectFromGrid();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }


}
