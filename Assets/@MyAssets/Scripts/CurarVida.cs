using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurarVida : MonoBehaviour {

    public GameObject jugador;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Curacion"))
        {
            
            if (jugador.GetComponent<PlayerLive>().getVida() < 100)
            {
                jugador.GetComponent<PlayerLive>().anyadirVida();
                Destroy(other.gameObject);
            }

        }
    }

}
