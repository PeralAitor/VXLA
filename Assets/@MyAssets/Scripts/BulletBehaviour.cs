using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletBehaviour : NetworkBehaviour
{

    public int damage = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (!IsServer) return;

        PlayerHealth player = other.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
            Destroy(gameObject); // O Despawn si usas NetworkObject
        }
    }
}
