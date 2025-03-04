using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importante para cambiar de escenas

public class TerminarEscena : MonoBehaviour
{

    public void FinishGame()
    {
        SceneManager.LoadScene(0);
    }
}
