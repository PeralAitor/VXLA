using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehaviour : MonoBehaviour
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
            pointsBehaviour.GetComponent<PointsBehaviour>().addPoints(20);
            SceneManager.LoadScene(2);
            Destroy(this.gameObject);
        }
    }
}
