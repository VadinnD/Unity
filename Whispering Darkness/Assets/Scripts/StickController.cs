using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickController : MonoBehaviour
{
    public Transform stick; // Ссылка на объект трости
    public float stickSpeed = 5f; // Скорость движения трости
    public float maxHeight = 2f; // Максимальная высота подъема трости
    public float dropHeight = 0.5f; // Высота, на которую трость должна опуститься
    public float returnSpeed = 2f; // Скорость возвращения трости на место
    public AudioClip soundEffect; // Аудиозапись для воспроизведения

    private bool isMovingUp = false; // Переменная для отслеживания направления движения вверх
    private bool isMovingDown = false; // Переменная для отслеживания направления движения вниз
    private bool isReturning = false; // Переменная для отслеживания возвращения в начальное положение
    private float originalYPosition; // Начальная позиция трости по оси Y
    private bool audioPlayed = false; // Переменная для отслеживания, была ли воспроизведена аудиозапись

    public PlayerLightController playerLightController; // Ссылка на контроллер света
    public PlayerMovement playerMovement; // Ссылка на контроллер движения персонажа
    public CameraController cameraController; // Ссылка на контроллер камеры


    void Start()
    {
        originalYPosition = stick.position.y; // Запоминаем начальную позицию трости по оси Y
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isMovingUp && !isMovingDown && !isReturning)
            {
                isMovingUp = true;
                playerMovement.DisableMovement(); // Блокируем движение
                cameraController.FreezeCamera(); // Замораживаем камеру
                Debug.Log("Start raising stick");
            }
        }

        if (isMovingUp)
        {
            // Перемещаем трость вверх с определенной скоростью только по оси Y
            stick.position = new Vector3(stick.position.x, stick.position.y + stickSpeed * Time.deltaTime, stick.position.z);

            // Проверяем, достигла ли трость нужной высоты
            if (stick.position.y >= originalYPosition + maxHeight)
            {
                isMovingUp = false;
                isMovingDown = true;
                Debug.Log("Stick raised to max height");
            }
        }

        if (isMovingDown)
        {
            // Перемещаем трость вниз с определенной скоростью только по оси Y
            stick.position = new Vector3(stick.position.x, stick.position.y - stickSpeed * Time.deltaTime, stick.position.z);

            // Проверяем, достигла ли трость нижней точки
            if (stick.position.y <= originalYPosition - dropHeight)
            {
                isMovingDown = false;
                if (!audioPlayed)
                {
                    // Воспроизводим аудиозапись на месте, где находится трость
                    AudioSource.PlayClipAtPoint(soundEffect, stick.position);
                    audioPlayed = true;
                    Debug.Log("Audio played");

                    if (playerLightController != null)
                    {
                        playerLightController.ActivateLight();
                    }
                    
                    // Добавляем задержку перед возвратом трости
                    Invoke("StartReturning", 2f);
                }
            }
        }

        if (isReturning)
        {
            // Перемещаем трость обратно на начальную позицию
            stick.position = new Vector3(stick.position.x, Mathf.MoveTowards(stick.position.y, originalYPosition, returnSpeed * Time.deltaTime), stick.position.z);
            if (stick.position.y == originalYPosition)
            {
                isReturning = false;
                audioPlayed = false; // Сбрасываем флаг для следующего опускания
                playerMovement.EnableMovement(); // Разблокируем движение
                cameraController.UnfreezeCamera();
                Debug.Log("Stick returned to original position");
            }
        }
    }

    // Метод для начала возврата трости на начальную позицию
    void StartReturning()
    {
        isReturning = true;
        Debug.Log("Start returning stick");
    }
}
