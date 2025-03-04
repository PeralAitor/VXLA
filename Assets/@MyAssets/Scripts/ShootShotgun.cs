using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ShootShotgun : MonoBehaviour
{
    public AudioSource audioSource;
    private bool shooting = false;
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform shootPoint;// Acci�n configurada para el disparo

    public int numProjectiles = 5; // N�mero de perdigones (balas)
    public float bulletSpeed = 30f; // Velocidad de las balas

    private float recoil = 0.7f;
    public GameObject recoilObj;

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
            float randomSpread1 = Random.Range(-0.15f, 0.15f);
            float randomSpread2 = Random.Range(-0.15f, 0.15f);
            float randomSpread3 = Random.Range(-0.15f, 0.15f);
            float randomSpread4 = Random.Range(-0.15f, 0.15f);

            // Define las direcciones de las 5 balas (un patr�n fijo)
            Vector3[] spreadDirections = new Vector3[]
            {
            shootPoint.forward, // Bala recta
            shootPoint.forward + shootPoint.right * randomSpread1, // Bala ligeramente a la derecha
            shootPoint.forward - shootPoint.right * randomSpread2, // Bala ligeramente a la izquierda
            shootPoint.forward + shootPoint.up * randomSpread3, // Bala ligeramente arriba
            shootPoint.forward - shootPoint.up * randomSpread4 // Bala ligeramente abajo
            };

            // Disparar m�ltiples balas (5 perdigones)
            for (int i = 0; i < numProjectiles; i++)
            {
                // Instancia la bala en la posici�n y rotaci�n del punto de disparo
                GameObject bullet = Instantiate(bulletPrefab);
                bullet.transform.position = shootPoint.position;
                bullet.GetComponent<Rigidbody>().velocity = spreadDirections[i].normalized * 20;

            }
            audioSource.Play();
            StartCoroutine(canShoot());
        }
    }

    private IEnumerator canShoot()
    {
        shooting = true;
        yield return new WaitForSeconds(recoil);
        shooting = false;
    }
}
