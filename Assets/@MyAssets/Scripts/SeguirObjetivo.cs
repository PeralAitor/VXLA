using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirObjetivo : MonoBehaviour
{
    public float velocidad = 3f;  // Velocidad de movimiento hacia el objetivo
    public float distanciaRayos = 2f;  // Distancia del raycast para detectar obst�culos
    public float anguloRayo = 45f;  // �ngulo del rayo de esquiva

    private Transform objetivo;

    // M�todo para inicializar el objetivo a seguir
    public void Inicializar(GameObject obj)
    {
        objetivo = obj.transform;
    }

    void Update()
    {
        if (objetivo != null)
        {
            // Verifica si hay un obst�culo en el camino
            Vector3 direccionHaciaObjetivo = (objetivo.position - transform.position).normalized;

            // Raycast para detectar obst�culos al frente
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direccionHaciaObjetivo, out hit, distanciaRayos))
            {
                // Si detecta un obst�culo, intenta esquivar
                EsquivarObstaculo(hit);
            }
            else
            {
                // Si no hay obst�culo, sigue al objetivo
                transform.position = Vector3.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);
            }
        }
    }

    void EsquivarObstaculo(RaycastHit hit)
    {
        // Si detecta un obst�culo, intenta esquivar hacia la derecha o izquierda
        Vector3 direccionEsquiva = Vector3.zero;

        // Intentar rayos hacia los lados para esquivar
        RaycastHit hitDerecha, hitIzquierda;

        if (!Physics.Raycast(transform.position, Quaternion.Euler(0, anguloRayo, 0) * (hit.point - transform.position).normalized, out hitDerecha, distanciaRayos))
        {
            direccionEsquiva = Quaternion.Euler(0, anguloRayo, 0) * (hit.point - transform.position).normalized; // Desviarse a la derecha
        }
        else if (!Physics.Raycast(transform.position, Quaternion.Euler(0, -anguloRayo, 0) * (hit.point - transform.position).normalized, out hitIzquierda, distanciaRayos))
        {
            direccionEsquiva = Quaternion.Euler(0, -anguloRayo, 0) * (hit.point - transform.position).normalized; // Desviarse a la izquierda
        }

        // Si se encontr� una direcci�n para esquivar, moverse en esa direcci�n
        if (direccionEsquiva != Vector3.zero)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direccionEsquiva, velocidad * Time.deltaTime);
        }
        else
        {
            // Si no se pudo esquivar, simplemente retrocede
            transform.position = Vector3.MoveTowards(transform.position, transform.position - direccionEsquiva, velocidad * Time.deltaTime);
        }
    }
}
