using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ShootAsault : MonoBehaviour
{
    public AudioSource audioSource;
    private bool shooting = false;
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform shootPoint;  // Acci�n configurada para el disparo
    private float recoil = 0.7f;
    public GameObject recoilObj;
     // Controla si se puede disparar

    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(ShootBullet);
    }

    private void Update()
    {
        recoil = recoilObj.GetComponent<GunsRecoil>().getRecoil();
    }
    public void ShootBullet(ActivateEventArgs arg)
    {
        if (!shooting)
        {
            StartCoroutine(delay());
            StartCoroutine(canShoot());
        }
    }

    public IEnumerator canShoot()
    {
        shooting = true;
        yield return new WaitForSeconds(recoil);
        shooting = false;
    }

    public IEnumerator delay() {
        for (int i = 0; i < 3; i ++) {
            GameObject spawnedBullet = Instantiate(bulletPrefab);
            spawnedBullet.transform.position = shootPoint.position;
            spawnedBullet.GetComponent<Rigidbody>().velocity = shootPoint.forward * 20;
            audioSource.Play();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
