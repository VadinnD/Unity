using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    public float maxRadius = 5f; // максимальный радиус сферы
    public float speed = 1f; // скорость расширения сферы

    public float returnSpeed = 2f; // скорость возвращения к начальному размеру
    public float stopDuration = 1f; // длительность остановки

    public WallController[] wallControllers; // контроллеры стен

    private float currentRadius = 0f;
    private bool isExpanding = false;

    private Vector3 initialScale; // начальный размер сферы

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (isExpanding)
        {
            currentRadius += speed * Time.deltaTime;
            transform.localScale = Vector3.one * currentRadius * 2f; // установка радиуса сферы

            if (currentRadius >= maxRadius)
            {
                isExpanding = false;
                ShowWalls(); // показываем стены при достижении сферой максимального размера
                StartCoroutine(ReturnToInitialSizeAfterDelay()); // вызываем корутину для возвращения к начальному размеру
            }
        }

        // Проверяем, была ли нажата кнопка "E"
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartExpanding();
        }
    }

    public void StartExpanding()
    {
        isExpanding = true;
        currentRadius = 0f;
    }

    IEnumerator ReturnToInitialSizeAfterDelay()
    {
        yield return new WaitForSeconds(stopDuration); // ждем определенное время
        StartCoroutine(ReturnToInitialSize()); // вызываем корутину для возвращения к начальному размеру после остановки
    }

    IEnumerator ReturnToInitialSize()
    {
        while (transform.localScale.magnitude > initialScale.magnitude)
        {
            transform.localScale -= Vector3.one * returnSpeed * Time.deltaTime;
            yield return null;
        }
        transform.localScale = initialScale;
        HideWalls(); // скрываем стены после возвращения сферы к начальному размеру
    }

    void ShowWalls()
    {
        foreach (var wallController in wallControllers)
        {
            wallController.MakeVisible(); // делаем стены видимыми
        }
    }

    void HideWalls()
    {
        foreach (var wallController in wallControllers)
        {
            wallController.MakeInvisible(); // скрываем стены
        }
    }
}

