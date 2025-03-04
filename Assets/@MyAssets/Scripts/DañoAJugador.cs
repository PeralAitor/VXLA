using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoAJugador : MonoBehaviour
{

    public GameObject AJugador;
    private bool restarVida;
    // Start is called before the first frame update
    void Start()
    {
         restarVida = false;
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (!restarVida) { 
                StartCoroutine(restarVidaZombieNormal());
            }
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            if (!restarVida) {
                StartCoroutine(restarVidaBossFinal());
            }
        }
    }
    IEnumerator restarVidaZombieNormal()
    {
        restarVida = true;
        AJugador.GetComponent<PlayerLive>().restarVida(10);
        yield return new WaitForSeconds(1);
        restarVida = false;
    }
    IEnumerator restarVidaBossFinal()
    {
        restarVida = true;
        AJugador.GetComponent<PlayerLive>().restarVida(20);
        yield return new WaitForSeconds(1);
        restarVida = false;
    }

}
