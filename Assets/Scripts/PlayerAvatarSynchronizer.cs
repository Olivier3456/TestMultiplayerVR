using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerAvatarSynchronizer : NetworkBehaviour
{
    private Transform realHeadTransform;
    private Transform realLeftHandTransform;
    private Transform realRightHandTransform;

    [SerializeField] private Transform avatarHeadTransform;
    [SerializeField] private Transform avatarLeftHandTransform;
    [SerializeField] private Transform avatarRightHandTransform;


    // To hide visual if this is our own avatar.
    [SerializeField] private Transform avatarVisualHeadTransform;
    [SerializeField] private Transform avatarVisualLeftHandTransform;
    [SerializeField] private Transform avatarVisualRightHandTransform;


    public override void OnNetworkSpawn()
    {
        if (NetworkManager.Singleton.LocalClientId == OwnerClientId)
        {
            // This is our own avatar, we don't want to see it. 
            avatarVisualHeadTransform.gameObject.SetActive(false);
            avatarVisualLeftHandTransform.gameObject.SetActive(false);
            avatarVisualRightHandTransform.gameObject.SetActive(false);
        }

        (Transform head, Transform leftHand, Transform rightHand) playerTransforms = GetPlayerHeadHandsPositions.Instance.Get();
        realHeadTransform = playerTransforms.head;
        realLeftHandTransform = playerTransforms.leftHand;
        realRightHandTransform = playerTransforms.rightHand;

        // NetworkObject networkObject = gameObject.GetComponent<NetworkObject>();
        // networkObject.Spawn();
    }


    private void Update()
    {
        if (!IsSpawned) return; // is this check necessarry?
        if (!IsOwner) return;

        avatarHeadTransform.position = realHeadTransform.position;
        avatarHeadTransform.rotation = realHeadTransform.rotation;

        avatarLeftHandTransform.position = realLeftHandTransform.position;
        avatarLeftHandTransform.rotation = realLeftHandTransform.rotation;

        avatarRightHandTransform.position = realRightHandTransform.position;
        avatarRightHandTransform.rotation = realRightHandTransform.rotation;
    }
}
