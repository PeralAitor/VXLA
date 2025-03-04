using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objetosPrefab;  // Lista de objetos a generar
    public float tiempoEntreSpawns = 2f;  // Tiempo entre spawns
    public Transform posicion;
    public GameObject objetivo;  // El GameObject que los objetos seguir�n

    private float tiempoUltimoSpawn = 0f;

    void Update()
    {
        // Verifica si ha pasado el tiempo suficiente para generar un nuevo objeto
        if (Time.time - tiempoUltimoSpawn >= tiempoEntreSpawns)
        {
            Spawn();
            tiempoUltimoSpawn = Time.time;  // Actualiza el tiempo del �ltimo spawn
        }
    }

    void Spawn()
    {
        // Selecciona un objeto aleatorio del array de prefabs
        int indiceAleatorio = Random.Range(0, objetosPrefab.Length);
        GameObject objetoAInstanciar = objetosPrefab[indiceAleatorio];

        // Instancia el objeto en la posici�n calculada
        GameObject objetoInstanciado = Instantiate(objetoAInstanciar, posicion.position, Quaternion.identity);

        // Si hay un objetivo, el objeto instanciado lo seguir�
        if (objetivo != null)
        {
            // Llama a la funci�n que hace que el objeto siga al objetivo
            objetoInstanciado.GetComponent<SeguirObjetivo>().Inicializar(objetivo);
        }
    }
}

