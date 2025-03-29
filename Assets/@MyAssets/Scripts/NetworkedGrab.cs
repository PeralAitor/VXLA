using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(NetworkObject), typeof(XRGrabInteractable))]
public class NetworkedGrab : NetworkBehaviour
{
    private NetworkObject m_NetworkObject;
    private XRGrabInteractable m_Interactable;

    void Awake()
    {
        m_NetworkObject = GetComponent<NetworkObject>();
        m_Interactable = GetComponent<XRGrabInteractable>();

        m_Interactable.selectEntered.AddListener((args) => RequestOwnership());
        m_Interactable.selectExited.AddListener((args) => ReleaseOwnership());
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
        m_NetworkObject.ChangeOwnership(clientId);
    }

    [Rpc(SendTo.Server)]
    private void ReleaseOwnershipRpc()
    {
        m_NetworkObject.RemoveOwnership();
    }
}
