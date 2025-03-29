using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(NetworkObject), typeof(XRGrabInteractable))]
public class NetworkedGrab : NetworkBehaviour
{
    private NetworkObject networkObject;
    private XRGrabInteractable interactable;

    void Awake()
    {
        networkObject = GetComponent<NetworkObject>();
        interactable = GetComponent<XRGrabInteractable>();

        interactable.selectEntered.AddListener((args) => RequestOwnership());
        interactable.selectExited.AddListener((args) => ReleaseOwnership());
    }

    private void RequestOwnership()
    {
        ulong clientId = NetworkManager.Singleton.LocalClientId;
        RequestOwnershipRpc(clientId);
    }

    private void ReleaseOwnership()
    {
        ReleaseOwnershipRpc();
    }

    [Rpc(SendTo.Server)]
    private void RequestOwnershipRpc(ulong clientId)
    {
        networkObject.ChangeOwnership(clientId);
    }

    [Rpc(SendTo.Server)]
    private void ReleaseOwnershipRpc()
    {
        networkObject.RemoveOwnership();
    }
}
