using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : NetworkBehaviour
{
    public int maxHealth = 100;
    public NetworkVariable<int> health = new NetworkVariable<int>();

    [Header("UI")]
    public TextMeshProUGUI healthText; // Texto para mostrar la vida

    private void Start()
    {
        if (IsServer)
            health.Value = maxHealth;

        UpdateHealthUI(health.Value);

        // Actualizar la UI en todos los clientes cuando la salud cambie.
        health.OnValueChanged += (oldVal, newVal) => UpdateHealthUI(newVal);
    }

    private void UpdateHealthUI(int currentHealth)
    {
        if (healthText != null)
            healthText.text = currentHealth.ToString();
    }

    public void TakeDamage(int damage)
    {
        if (!IsServer) return;

        health.Value -= damage;

        if (health.Value <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log($"Jugador {OwnerClientId} ha muerto.");

        // Enviar a la escena "YouLose" al jugador que murió:
        LoadSceneClientRpc("YouLose", new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new List<ulong> { OwnerClientId }
            }
        });

        // Enviar a la escena "YouWin" a los demás jugadores (incluido el host si no es el que murió):
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            if (client.ClientId != OwnerClientId)
            {
                LoadSceneClientRpc("YouWin", new ClientRpcParams
                {
                    Send = new ClientRpcSendParams
                    {
                        TargetClientIds = new List<ulong> { client.ClientId }
                    }
                });
            }
        }
    }

    [ClientRpc]
    private void LoadSceneClientRpc(string sceneName, ClientRpcParams clientRpcParams = default)
    {
        SceneManager.LoadScene(sceneName);
    }
}
