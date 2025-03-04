using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointsBehaviour : MonoBehaviour
{
    public static int points;
    public TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        textMeshPro.text = "Puntos: " + points;
    }

    public void addPoints(int pointsToAdd)
    {
        points += pointsToAdd;
    }

    public int getPoints() { 
        return points;
    }

    public void changePoints(int puntos)
    {
        points = puntos;
    }
}
