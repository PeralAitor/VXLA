using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importante para cambiar de escenas

public class CambiaEscena : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}


