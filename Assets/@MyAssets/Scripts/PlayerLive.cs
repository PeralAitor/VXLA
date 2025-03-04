using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLive : MonoBehaviour
{
    public static int vidaJugador;
    public TextMeshProUGUI textMeshPro;
    // Start is called before the first frame update
    void Start()
    {
        vidaJugador = 100;
    }

    private void Update()
    {
        textMeshPro.text = "Vida: " + vidaJugador;
        if ( vidaJugador <= 0)
        {
            Muerte();
        }
    }

    public void restarVida(int vidaARestar)
    {
        if (vidaJugador > 0)
        {
            vidaJugador -= vidaARestar;
        }
    }

    void Muerte()
    {
        SceneManager.LoadScene(3);
    }

    public void anyadirVida()
    {
        if (vidaJugador < 100)
        {
            vidaJugador = 100;
        }
    }
    public int getVida()
    {
        return vidaJugador;
    }
}
