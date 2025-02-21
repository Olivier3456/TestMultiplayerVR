using UnityEngine;
using Unity.Netcode;
using System;


public struct PlayerAvatarData : INetworkSerializable
{
    public ulong ownerId;
    public Vector3 headPosition;
    public Vector3 headRotation;

    public Vector3 leftHandPosition;
    public Vector3 leftHandRotation;

    public Vector3 rightHandPosition;
    public Vector3 rightHandRotation;


    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ownerId);
        serializer.SerializeValue(ref headPosition);
        serializer.SerializeValue(ref headRotation);
        serializer.SerializeValue(ref leftHandPosition);
        serializer.SerializeValue(ref leftHandRotation);
        serializer.SerializeValue(ref rightHandPosition);
        serializer.SerializeValue(ref rightHandRotation);
    }
}



public class PlayerAvatarSender : NetworkBehaviour
{
    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private Transform rightHandTransform;


    private NetworkVariable<PlayerAvatarData> netVar_playerAvatarData = new NetworkVariable<PlayerAvatarData>(
                                                                                    new PlayerAvatarData(),
                                                                                    NetworkVariableReadPermission.Everyone,
                                                                                    NetworkVariableWritePermission.Owner);


    public NetworkVariable<PlayerAvatarData> NetVar_playerAvatarData => netVar_playerAvatarData;


    public override void OnNetworkSpawn()
    {
        Debug.Log("Player avatar spawned.");

        netVar_playerAvatarData.OnValueChanged += NetVar_playerAvatarData_OnValueChanged;
    }


    private void NetVar_playerAvatarData_OnValueChanged(PlayerAvatarData previousValue, PlayerAvatarData newValue)
    {
        PlayersAvatarsManager.Instance.OnPlayerAvatarUpdate(newValue);
    }


    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        netVar_playerAvatarData.Value = new PlayerAvatarData
        {
            ownerId = NetworkManager.Singleton.LocalClientId,

            headPosition = headTransform.position,
            headRotation = headTransform.rotation.eulerAngles,

            leftHandPosition = leftHandTransform.position,
            leftHandRotation = leftHandTransform.rotation.eulerAngles,

            rightHandPosition = rightHandTransform.position,
            rightHandRotation = rightHandTransform.rotation.eulerAngles,
        };
    }
}
