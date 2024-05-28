using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnCollision : MonoBehaviour
{
    public Material changingMaterial; // Материал для изменения цвета стены
    public Color newColor; // Новый цвет для стены

    private Material originalMaterial; // Исходный материал стены

    void Start()
    {
        originalMaterial = GetComponent<Renderer>().material;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что столкнулась именно сфера
        {
            GetComponent<Renderer>().material = changingMaterial; // Меняем материал стены
            changingMaterial.color = newColor; // Устанавливаем новый цвет
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Проверяем, что сфера вышла из стены
        {
            GetComponent<Renderer>().material = originalMaterial; // Возвращаем исходный материал стены
        }
    }
}
