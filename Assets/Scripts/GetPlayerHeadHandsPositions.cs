using UnityEngine;

public class GetPlayerHeadHandsPositions : MonoBehaviour
{
    public static GetPlayerHeadHandsPositions Instance { get; private set; }

    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform leftHandTransform;
    [SerializeField] private Transform rightHandTransform;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Only one instance of GetPlayerHeadHandsPositions is allowed per player!");
            Destroy(this);
            return;
        }
    }


    public (Transform head, Transform leftHand, Transform rightHand) Get()
    {
        return (headTransform, leftHandTransform, rightHandTransform);
    }
}
