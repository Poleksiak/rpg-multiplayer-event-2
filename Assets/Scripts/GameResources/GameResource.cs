using UnityEngine;

// Ten atrybut pozwala nam tworzy� nowe instancje GameResource za pomoc� edytora Unity.
[CreateAssetMenu(fileName = "NewGameResource", menuName = "Game Resources/GameResource")]
public class GameResource : ScriptableObject
{
    // Nazwa GameResource. Mo�esz dostosowa� to, aby zawiera�o bardziej szczeg�owe informacje.
    public string resourceName;

    // Referencja do Prefabu. Pozwala to na skojarzenie okre�lonego prefabrykatu z tym GameResource.
    public GameObject prefab;
}
