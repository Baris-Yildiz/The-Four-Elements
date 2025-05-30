// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2025 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using UnityEngine;

namespace Animancer.Samples.InverseKinematics
{
    /// <summary>Records the positions and rotations of a set of objects so they can be returned later on.</summary>
    /// 
    /// <remarks>
    /// <strong>Sample:</strong>
    /// <see href="https://kybernetik.com.au/animancer/docs/samples/ik/puppet">
    /// Puppet</see>
    /// </remarks>
    /// 
    /// https://kybernetik.com.au/animancer/api/Animancer.Samples.InverseKinematics/TransformResetter
    /// 
    [AddComponentMenu(Strings.SamplesMenuPrefix + "Inverse Kinematics - Transform Resetter")]
    [AnimancerHelpUrl(typeof(TransformResetter))]
    public class TransformResetter : MonoBehaviour
    {
        /************************************************************************************************************************/

        [SerializeField] private Transform[] _Transforms;

        private Vector3[] _StartingPositions;
        private Quaternion[] _StartingRotations;

        /************************************************************************************************************************/

        protected virtual void Awake()
        {
            int count = _Transforms.Length;
            _StartingPositions = new Vector3[count];
            _StartingRotations = new Quaternion[count];
            for (int i = 0; i < count; i++)
            {
                Transform transform = _Transforms[i];
                _StartingPositions[i] = transform.localPosition;
                _StartingRotations[i] = transform.localRotation;
            }
        }

        /************************************************************************************************************************/

        // Called by a UI Button.
        // This method is not called Reset because that is a MonoBehaviour message (like Awake).
        // That would cause Unity to call it in Edit Mode when we first add this component.
        // And since the _StartingPositions would be null it would throw a NullReferenceException.
        public void ReturnToStartingValues()
        {
            for (int i = 0; i < _Transforms.Length; i++)
                _Transforms[i].SetLocalPositionAndRotation(_StartingPositions[i], _StartingRotations[i]);
        }

        /************************************************************************************************************************/
    }
}
