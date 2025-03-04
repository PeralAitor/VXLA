using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehaviour : MonoBehaviour
{
    public int health = 3;
    public GameObject pointsBehaviour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hit()
    {
        health--;
        if (health <= 0)
        {
            pointsBehaviour.GetComponent<PointsBehaviour>().addPoints(10);
            Destroy(this.gameObject);
        }
    }
}
