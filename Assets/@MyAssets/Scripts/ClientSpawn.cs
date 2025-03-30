using UnityEngine;
using Unity.Netcode;

public class ClientSpawn : NetworkBehaviour
{
    [SerializeField] private Transform spawnClient;
    [SerializeField] private Transform spawnHost;
    [SerializeField] private GameObject playerPrefab;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) return;

        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        int playerCount = NetworkManager.Singleton.ConnectedClients.Count;

        foreach (var clientId in NetworkManager.Singleton.ConnectedClientsIds)
        {
            Vector3 spawnPosition = clientId == NetworkManager.ServerClientId ?
                spawnHost.position :
                spawnClient.position;

            GameObject playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
            playerInstance.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
        }
    }
}