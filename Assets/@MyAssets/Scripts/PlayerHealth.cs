using Unity.Netcode;
using UnityEngine;
using TMPro;

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

        // Se actualiza en todos los clientes cuando cambia la salud
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
        // Aquí puedes añadir lógica de muerte o respawn
    }
}
