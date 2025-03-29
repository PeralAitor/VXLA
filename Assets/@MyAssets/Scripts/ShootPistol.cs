using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.Netcode;

public class ShootPistol : NetworkBehaviour
{
        public AudioSource audioSource;
        private bool shooting = false;
        public GameObject bulletPrefab; // Prefab de la bala
        public Transform shootPoint;    // Punto desde donde se dispara (puedes usar this.transform si no tienes uno espec�fico)
        private float recoil = 0.7f;
        public GameObject recoilObj;


    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(ShootBullet);
    }

    // Update is called once per frame
    void Update()
    {
        recoil = recoilObj.GetComponent<GunsRecoil>().getRecoil();
    }

    public void ShootBullet(ActivateEventArgs arg)
    {
        shootRpc();
    }

    [Rpc(SendTo.Server)]
    private void shootRpc()
    {
        if (!shooting)
        {
            GameObject spawnedBullet = Instantiate(bulletPrefab);
            spawnedBullet.GetComponent<NetworkObject>().Spawn();
            spawnedBullet.transform.position = shootPoint.position;
            spawnedBullet.GetComponent<Rigidbody>().velocity = shootPoint.forward * 20;
            audioSource.Play();
            StartCoroutine(canShoot());
        }
    }

    public IEnumerator canShoot()
    {
        shooting = true;
        yield return new WaitForSeconds(recoil);
        shooting = false;
    }
}
