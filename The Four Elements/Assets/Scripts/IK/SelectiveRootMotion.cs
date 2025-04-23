using UnityEngine;

/// <summary>
/// This script overrides the default Animator root motion application.
/// It applies the animation's root rotation and vertical (Y) position change,
/// but ignores horizontal (X and Z) position changes.
/// Attach this to the GameObject with the Animator and CharacterController.
/// The Animator must have 'Apply Root Motion' enabled for OnAnimatorMove to be called.
/// </summary>
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))] // Or Rigidbody if using that
public class SelectiveRootMotion : MonoBehaviour
{
    private Animator _animator;
    private CharacterController _characterController;
    // private Rigidbody _rigidbody; // Use this if you move with Rigidbody physics

    void Awake()
    {
        
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        if (_animator == null)
            Debug.LogError("SelectiveRootMotion requires an Animator component.", this);
        if (_characterController == null /* && _rigidbody == null */) // Adapt if using Rigidbody
            Debug.LogError("SelectiveRootMotion requires a CharacterController (or Rigidbody) component.", this);
    }

    // This method is called by Unity automatically every frame IF the Animator
    // has 'Apply Root Motion' enabled AND this script component exists and is enabled.
    // Implementing this method disables the default root motion behaviour.
    void OnAnimatorMove()
    {
        // Ensure components are valid and root motion is intended
        if (_animator == null || !_animator.applyRootMotion || Time.deltaTime == 0)
        {
            return; 
        }

        Quaternion deltaRotation = _animator.deltaRotation;
 
        transform.rotation *= deltaRotation;

        Vector3 deltaPosition = _animator.deltaPosition;

        Vector3 verticalMovement = new Vector3(0f, deltaPosition.y, 0f);

        if (_characterController != null && _characterController.enabled)
        {
            _characterController.Move(verticalMovement);
        }
        
    }
}
