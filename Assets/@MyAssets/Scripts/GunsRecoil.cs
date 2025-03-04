using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsRecoil : MonoBehaviour
{
    private float recoil; 

    // Start is called before the first frame update
    void Start()
    {
        recoil = 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mejorarRecoil()
    {
        if (recoil > 0.0f)
        {
            recoil -= 0.05f;
        } 
    }

    public float getRecoil()
    {
        return recoil;  
    }
}
