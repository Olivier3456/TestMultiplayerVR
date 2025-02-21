// using Unity.Collections;
// using Unity.Netcode;
// using UnityEngine;


// // Inherits from NetworkBehaviour instead of MonoBehaviour.
// public class PlayerNetwork : NetworkBehaviour
// {
//     // initialize NetworkVariable at declaration
//     private NetworkVariable<MyCustomData> myCustomData = new NetworkVariable<MyCustomData>(
//                                                                          new MyCustomData { _int = 1, _bool = false },
//                                                                          NetworkVariableReadPermission.Everyone,
//                                                                          NetworkVariableWritePermission.Owner);
//     // by default, it is: read by everyone, write only by server


//     [SerializeField] private Transform spawnedObjectPrefab;
//     private Transform spawnedObjectTransform;


//     public struct MyCustomData : INetworkSerializable
//     {
//         public int _int;
//         public bool _bool;

//         // String is ref type, so we can't use it, but we can use FixedString.
//         // 128Bytes is for the max length of the string: here, 128 signs.
//         // Works from 32 to 4096 bytes.
//         public FixedString128Bytes message;

//         public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
//         {
//             serializer.SerializeValue(ref _int);
//             serializer.SerializeValue(ref _bool);
//             serializer.SerializeValue(ref message);
//         }
//     }


//     public override void OnNetworkSpawn()
//     {
//         myCustomData.OnValueChanged += (MyCustomData previousValue, MyCustomData newValue) =>
//         {
//             Debug.Log($"Client with Id {OwnerClientId} just changed MyCustomData. Previous _int value was: {previousValue._int}. New _int value is: {newValue._int}. Message is: {newValue.message}");
//         };
//     }


//     private void Update()
//     {
//         if (!IsOwner) return;

//         if (Input.GetKeyDown(KeyCode.T))
//         {
//             myCustomData.Value = new MyCustomData { _int = Random.Range(0, 100), _bool = false, message = "You have a new message!" };

//             // spawnedObjectTransform = Instantiate(spawnedObjectPrefab);
//             // spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);
//             // true for: destroy with scene
//         }

//         if (Input.GetKeyDown(KeyCode.Y))
//         {
//             Destroy(spawnedObjectTransform.gameObject);

//             // ulong targetClientId = 1;
//             // TestOtherRpc(RpcTarget.Single(targetClientId, RpcTargetUse.Temp));
//         }



//         Vector3 moveDir = new Vector3(0, 0, 0);

//         if (Input.GetKey(KeyCode.W)) moveDir.z = +1f;
//         if (Input.GetKey(KeyCode.S)) moveDir.z = -1f;
//         if (Input.GetKey(KeyCode.A)) moveDir.x = -1f;
//         if (Input.GetKey(KeyCode.D)) moveDir.x = +1f;

//         float moveSpeed = 3f;

//         transform.position += moveDir * moveSpeed * Time.deltaTime;
//     }


//     // Here, the rpc will execute HOST (= server) side, even if triggered by a CLIENT!
//     // But OwnerClientId is the one of the instance calling the function
//     // (=> client if called by a client)
//     [Rpc(SendTo.Server)]
//     private void TestRpc() // rpc function name must finish by rpc
//     {
//         Debug.Log($"{OwnerClientId} called function TestRpc.");
//     }

//     [Rpc(SendTo.SpecifiedInParams)]
//     private void TestOtherRpc(RpcParams rpcParams = default)
//     {
//         Debug.Log($"{OwnerClientId} called function TestOtherRpc.");
//     }
// }