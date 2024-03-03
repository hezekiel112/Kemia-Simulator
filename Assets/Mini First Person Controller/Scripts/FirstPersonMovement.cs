using System.Collections.Generic;
using UnityEngine;

namespace KemiaSimulatorEnvironment.Player {
    public class FirstPersonMovement : MonoBehaviour {
        [Header("Running")]
        [SerializeField] bool _canRun = true;

        readonly float WalkSpeed = 5;
        readonly float RunSpeed = 9;
        readonly KeyCode RunningKey = KeyCode.LeftShift;
        [SerializeField] Rigidbody _rigidbody;
        public readonly List<System.Func<float>> SpeedOverrides = new List<System.Func<float>>();

        float _targetMovingSpeed = 0;

        public bool IsRunning {
            get; private set;
        }

        private void LateUpdate() {
            // Update IsRunning from input.
            IsRunning = _canRun && Input.GetKey(RunningKey);
            // Get targetMovingSpeed.
            _targetMovingSpeed = IsRunning ? RunSpeed : WalkSpeed;
        }

        void FixedUpdate() {
            if (SpeedOverrides.Count > 0)
                _targetMovingSpeed = SpeedOverrides[^1]();

            // Get targetVelocity from input.
            Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * _targetMovingSpeed, Input.GetAxis("Vertical") * _targetMovingSpeed);

            // Apply movement.
            _rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, _rigidbody.velocity.y, targetVelocity.y);
        }
    }
}