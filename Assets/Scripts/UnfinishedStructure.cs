using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

//Komponent s�u��cdo budowy struktur
public class UnfinishedStructure : MonoBehaviour
{
    //Zmienne z Property Notify
    //Ilo�� zasobu w budynku
    private int resourceAmount = 0;

    //Tutaj mamy RaisePropertyChanged po to by m�c poinformowa� o zmianie ilo�ci zasob�w - taki databinding
    public int ResourceAmount
    {
        get => resourceAmount;
        set
        {
            resourceAmount = value;
            RaisePropertyChanged("ResourceAmount");
        }
    }

    //Element aktywuj�cy generator
    public InteractiveElement activator;
    //Zas�b potrzebny do zbudowania struktury
    public GameResource neededResource;
    //Prefab zbudowanej struktury
    public GameObject finishedStructurePrefab;
    //Miejsce gdzie ma by� zbudowana struktura
    public Transform finishedStructureLocation;
    //Ilo�� zasobu potrzebnego do zbudowania struktury
    public int neededResourceAmount;
    [SerializeField]
    //Czy trwa transfer zasob�w - aktywuje si� to kiedy gracz wejdzie na p�ytk� aktywacyjn� / uruchomi aktywator
    private bool inTransferActivated;
    [SerializeField]
    //Timer do transferu zasob�w - bo tranfer nie jest automatyczny tylko odbywa si� co jaki� czas z op�nieniem
    private float inTransferTimer = 0f;
    [SerializeField]
    //Op�nienie transferu zasob�w - to co wy�ej
    private float inTransferDelay = 2f;
    [SerializeField]
    //Kolekcja zasob�w w budynku, grid do po�o�enia zasob�w obok budynku
    private GridGameObjectCollection resourcesDeposidGridCollection;

    [SerializeField]
    private GameObject minigamePrefab;
    private MinigameBase minigame;

    public event PropertyChangedEventHandler PropertyChanged;

    private void Awake()
    {
        //Subskrybujemy si� na zmiany w aktywatorze
        activator.PropertyChanged += OnPropertyChanged;
        //Subskrybujemy si� na zmiany w zasobach
        this.PropertyChanged += OnPropertyChanged;
        //Ustawiamy prefab zasobu w gridzie
        resourcesDeposidGridCollection.prefab = neededResource.prefab;
    }

    private void Update()
    {
        //Je�li aktywator transferu jest aktywowany i ilo�� zasob�w jest mniejsza ni� potrzebna ilo�� zasob�w to zaczynamy transfer
        if (inTransferActivated && ResourceAmount < neededResourceAmount)
        {
            inTransferTimer += Time.deltaTime;
            if (inTransferTimer >= inTransferDelay)
            {
                inTransferTimer = 0f;
                if (activator.PlayerGameObject.GetComponent<PlayerBackPack>().TransferOut(neededResource, 1))
                {
                    ResourceAmount += 1;
                }
                if (CheckIfCanBuild())
                {
                    minigame = MinigamesManager.Instance.StartMinigame(minigamePrefab);
                    minigame.actionPerformedEvent += BuildStructure;
                    minigame.closedEvent += () => minigame = null;
                }
            }
        }
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "IsActivated")
        {
            inTransferActivated = activator.IsActivated;
        }

        if(e.PropertyName == "ResourceAmount")
        {
            StartCoroutine(UpdateDepotStash());
        }
    }

    private void RaisePropertyChanged(string propertyName)
    {
        var propChange = PropertyChanged;
        if (propChange == null) return;
        propChange(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool CheckIfCanBuild()
    {
        if (neededResource == null)
        {
            Debug.LogError("Needed resource is not set");
            return false;
        }
        if (ResourceAmount < neededResourceAmount)
        {
            return false;
        }
        return true;
    }

    private void BuildStructure()
    {
        Instantiate(finishedStructurePrefab, finishedStructureLocation.position, finishedStructureLocation.rotation);
        Destroy(gameObject);
    }

    private IEnumerator UpdateDepotStash()
    {
        while (ResourceAmount != resourcesDeposidGridCollection.gridObjects.Count)
        {
            if (ResourceAmount > resourcesDeposidGridCollection.gridObjects.Count)
            {
                resourcesDeposidGridCollection.AddObjectToGrid();
            }
            else
            {
                resourcesDeposidGridCollection.RemoveObjectFromGrid();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}

