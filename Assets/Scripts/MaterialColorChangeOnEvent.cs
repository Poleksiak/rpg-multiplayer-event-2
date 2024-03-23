using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorChangeOnEvent : MonoBehaviour
{
    [Header("MeshRenderer z kt�rego bierzemy materia�")]
    public MeshRenderer meshRenderer;
    [Header("Kolor na kt�ry zmieni si� materia� w reakcji na zdarzenie")]
    public Color color;

    //T� metod� musimy pod��czy� do Eventu �rod�owego
    public void ChangeColor()
    {
        meshRenderer.material.color = color;
    }


}
