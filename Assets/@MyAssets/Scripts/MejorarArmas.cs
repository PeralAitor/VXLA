using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MejorarArmas : MonoBehaviour
{

    public TextMeshProUGUI texto;
    private bool armaMejorada = false;
    public int puntosMejorar = 50;
    public PointsBehaviour puntos;
    private int puntosJugador = 0;
    public GameObject recoilObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        puntosJugador = puntos.GetComponent<PointsBehaviour>().getPoints();

        if (armaMejorada)
        {
            texto.text = puntosMejorar + " Puntos";
            recoilObj.GetComponent<GunsRecoil>().mejorarRecoil();
            armaMejorada = false;  
        }
    }

    public void mejorar()
    {
        if (puntosJugador >= puntosMejorar)
        {
            puntosJugador -= puntosMejorar;
            puntosMejorar *= 2;
            puntos.GetComponent<PointsBehaviour>().changePoints(puntosJugador);
            armaMejorada = true;
        }
    }
}
