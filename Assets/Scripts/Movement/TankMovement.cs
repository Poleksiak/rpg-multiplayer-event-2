using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPlayerController : MonoBehaviour
{
    // Implementacja "tank controls". Posta� porusza si� do przodu wzgl�dem swojej rotacji, co pozwala zachowa� kierunek ruchu przy zmianie pozycji aktywnej kamery
    private CharacterController controller;
    private float vSpeed = 0;
    public float speed = 5f;
    public float turnSpeed = 180f;
    // CharacterController domy�lnie nie uwzgl�dnia grawitacji, bo steruje ruchem za pomoc� bezpo�rednich przemieszcze�. Grawitacj� trzeba uwzgl�dni� i nanie�� podczas wykonania Update()
    public float gravity = 9.8f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 moveDirection;

        // Obr�t postaci wok� osi Y
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
        // Obliczenie ruchu do przodu
        moveDirection = transform.forward * Input.GetAxis("Vertical") * speed;

        // Zastosuj grawitacj�
        vSpeed -= gravity * Time.deltaTime;
        moveDirection.y = vSpeed;
        controller.Move(moveDirection * Time.deltaTime);

    }
}
