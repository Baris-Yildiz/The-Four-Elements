using UnityEngine;
using Animancer; // Namespace might not be strictly needed, but good practice if using Animancer

// Ensures the necessary components are present
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AnimancerComponent))] // Good to require if Animancer is driving animations
public class HandIK : MonoBehaviour
{
    private Animator _animator;
    // No direct reference to AnimancerComponent needed for standard IK logic

    [Header("IK Settings")]
    [Tooltip("Which hand should this script control?")]
    [SerializeField] private AvatarIKGoal _ikGoal = AvatarIKGoal.RightHand;

    [Tooltip("Assign the Transform the hand should always track.")]
    [SerializeField] private Transform _ikTarget;

    [Tooltip("How strongly the hand should stick to the target's position (0 = animation only, 1 = fully locked).")]
    [SerializeField] [Range(0f, 1f)] private float _ikPositionWeight = 1.0f;

    [Tooltip("How strongly the hand should match the target's rotation (0 = animation only, 1 = fully locked).")]
    [SerializeField] [Range(0f, 1f)] private float _ikRotationWeight = 1.0f;

    [Header("Optional Elbow Hint")]
    [Tooltip("(Optional) Assign a Transform to guide the elbow's position.")]
    [SerializeField] private Transform _ikHint;
    [SerializeField] [Range(0f, 1f)] private float _ikHintWeight = 0.5f; // Often good to have some hint influence

    void Awake()
    {
        _animator = GetComponent<Animator>();

        if (_animator == null)
        {
            Debug.LogError("AlwaysOnHandIK requires an Animator component on the same GameObject.", this);
            enabled = false; // Disable this script if Animator is missing
            return;
        }

        if (_animator.avatar == null || !_animator.avatar.isHuman)
        {
             Debug.LogError("AlwaysOnHandIK requires the Animator to have a valid Humanoid Avatar.", this);
             enabled = false;
             return;
        }
    }

    // OnAnimatorIK is called by Unity automatically AFTER the base animation pose is calculated
    // but BEFORE the final IK adjustments are applied. This is the perfect place to set IK goals.
    // It's called even when Animancer is controlling the Animator.
    void OnAnimatorIK(int layerIndex)
    {
        // Always check if the Animator is valid and enabled
        if (_animator == null || !_animator.enabled)
        {
            return;
        }

        // If no target is assigned, disable IK for this frame
        if (_ikTarget == null)
        {
            _animator.SetIKPositionWeight(_ikGoal, 0);
            _animator.SetIKRotationWeight(_ikGoal, 0);
            if (_ikHint != null) SetHintWeight(0); // Reset hint weight too
            return;
        }

        // Set the influence of the IK target on the hand's position and rotation
        _animator.SetIKPositionWeight(_ikGoal, _ikPositionWeight);
        _animator.SetIKRotationWeight(_ikGoal, _ikRotationWeight);

        // Set the desired position and rotation for the hand to match the target
        _animator.SetIKPosition(_ikGoal, _ikTarget.position);
        _animator.SetIKRotation(_ikGoal, _ikTarget.rotation);

        // --- Optional: Handle Elbow Hint ---
        if (_ikHint != null)
        {
            SetHintWeight(_ikHintWeight);
            SetHintPosition(_ikHint.position);
        }
        else
        {
            // Ensure hint weight is 0 if no hint Transform is assigned
            SetHintWeight(0);
        }
        // --- ---
    }

    // Helper to set the correct hint weight based on the IK goal
    private void SetHintWeight(float weight)
    {
        AvatarIKHint hint = GetHintForGoal(_ikGoal);
        if (hint != AvatarIKHint.LeftKnee + 1) // Check if valid hint for goal
        {
             _animator.SetIKHintPositionWeight(hint, weight);
        }
    }

    // Helper to set the correct hint position based on the IK goal
    private void SetHintPosition(Vector3 position)
    {
        AvatarIKHint hint = GetHintForGoal(_ikGoal);
         if (hint != AvatarIKHint.LeftKnee + 1) // Check if valid hint for goal
        {
            _animator.SetIKHintPosition(hint, position);
        }
    }

    // Helper to determine the correct elbow/knee hint based on the hand/foot goal
    private AvatarIKHint GetHintForGoal(AvatarIKGoal goal)
    {
        switch (goal)
        {
            case AvatarIKGoal.LeftHand: return AvatarIKHint.LeftElbow;
            case AvatarIKGoal.RightHand: return AvatarIKHint.RightElbow;
            case AvatarIKGoal.LeftFoot: return AvatarIKHint.LeftKnee;
            case AvatarIKGoal.RightFoot: return AvatarIKHint.RightKnee;
            default: return AvatarIKHint.LeftKnee + 1; // Return an invalid value
        }
    }

     // Optional: Visualize the target and hint in the Scene view for easier setup
     void OnDrawGizmosSelected()
     {
         if (_ikTarget != null)
         {
             Gizmos.color = Color.cyan;
             Gizmos.DrawWireSphere(_ikTarget.position, 0.05f);
             Gizmos.DrawRay(_ikTarget.position, _ikTarget.forward * 0.2f); // Show target forward
         }
          if (_ikHint != null)
         {
             Gizmos.color = Color.magenta;
             Gizmos.DrawWireSphere(_ikHint.position, 0.03f);
              // Draw line from hint to relevant joint (requires finding the joint transform)
             // Transform hand = _animator?.GetBoneTransform( _ikGoal == AvatarIKGoal.LeftHand ? HumanBodyBones.LeftHand : HumanBodyBones.RightHand);
             // if(hand != null) Gizmos.DrawLine(_ikHint.position, hand.position);
         }
     }
}
