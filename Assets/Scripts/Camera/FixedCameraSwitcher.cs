using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCameraSwitcher : MonoBehaviour
{
    // Skrypt prze��czaj�cy mi�dzy kamerami, gdy posta� wejdzie w przypisan� do nich stref�
    public Transform Player;
    // Kamera, kt�r� steruje dana strefa
    public CinemachineVirtualCamera activeCam;
    // Delegate i Event, kt�re pozwalaj� nam na reakcj� na wej�cie u�ytkownika do strefy nowej kamery
    public delegate void OnPrioritySetDelegate();
    public event OnPrioritySetDelegate OnPrioritySet;

    private void OnTriggerEnter(Collider other)
    {
        // Je�eli gracz koliduje ze stref� kamery, podnie� jej priorytet...
        if (other.CompareTag("Player"))
        {
            if (OnPrioritySet != null)
            {
                OnPrioritySet();
            }
            activeCam.Priority = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ...a gdy z niej wychodzi, obni� go
        if (other.CompareTag("Player"))
        {
            activeCam.Priority = 0;
        }
    }

    private void Update()
    {
        //Debug.Log(name + " " + activeCam.Priority);       DLACZEGO TA LINIJKA???!!!
    }
}

    


