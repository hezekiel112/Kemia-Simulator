using UnityEngine;

namespace KemiaSimulatorEnvironment.Player {
    public class Crouch : MonoBehaviour {
        readonly KeyCode key = KeyCode.LeftControl;

        [Header("Slow Movement")]
        [Tooltip("Movement to slow down when crouched.")]
        [SerializeField] FirstPersonMovement _fpsMovement;
        [Tooltip("Movement speed when crouched.")]
        [SerializeField] float _movementSpeed = 2;

        [Header("Low Head")]
        [Tooltip("Head to lower when crouched.")]
        [SerializeField] Transform _headToLower;
        [HideInInspector]
        float? _defaultHeadYLocalPosition;
        readonly float  _crouchYHeadPosition = 1;

        [Tooltip("Collider to lower when crouched.")]
        [SerializeField] CapsuleCollider _colliderToLower;
        [HideInInspector]
        float? _defaultColliderHeight;

        public bool IsCrouched {
            get; private set;
        }
        public event System.Action CrouchStart, CrouchEnd;

        void LateUpdate() {
            if (Input.GetKey(key)) {
                // Enforce a low head.
                if (_headToLower) {
                    // If we don't have the defaultHeadYLocalPosition, get it now.
                    if (!_defaultHeadYLocalPosition.HasValue) {
                        _defaultHeadYLocalPosition = _headToLower.localPosition.y;
                    }

                    // Lower the head.
                    _headToLower.localPosition = new Vector3(_headToLower.localPosition.x, _crouchYHeadPosition, _headToLower.localPosition.z);
                }

                // Enforce a low colliderToLower.
                if (_colliderToLower) {
                    // If we don't have the defaultColliderHeight, get it now.
                    if (!_defaultColliderHeight.HasValue) {
                        _defaultColliderHeight = _colliderToLower.height;
                    }

                    // Get lowering amount.
                    float loweringAmount;
                    if (_defaultHeadYLocalPosition.HasValue) {
                        loweringAmount = _defaultHeadYLocalPosition.Value - _crouchYHeadPosition;
                    }
                    else {
                        loweringAmount = _defaultColliderHeight.Value * .5f;
                    }

                    // Lower the colliderToLower.
                    _colliderToLower.height = Mathf.Max(_defaultColliderHeight.Value - loweringAmount, 0);
                    _colliderToLower.center = Vector3.up * _colliderToLower.height * .5f;
                }

                // Set IsCrouched state.
                if (!IsCrouched) {
                    IsCrouched = true;
                    SetSpeedOverrideActive(true);
                    CrouchStart?.Invoke();
                }
            }
            else {
                if (IsCrouched) {
                    // Rise the head back up.
                    if (_headToLower) {
                        _headToLower.localPosition = new Vector3(_headToLower.localPosition.x, _defaultHeadYLocalPosition.Value, _headToLower.localPosition.z);
                    }

                    // Reset the colliderToLower's height.
                    if (_colliderToLower) {
                        _colliderToLower.height = _defaultColliderHeight.Value;
                        _colliderToLower.center = Vector3.up * _colliderToLower.height * .5f;
                    }

                    // Reset IsCrouched.
                    IsCrouched = false;
                    SetSpeedOverrideActive(false);
                    CrouchEnd?.Invoke();
                }
            }
        }


        #region Speed override.
        void SetSpeedOverrideActive(bool state) {
            // Stop if there is no movement component.
            if (!_fpsMovement) {
                return;
            }

            // Update SpeedOverride.
            if (state) {
                // Try to add the SpeedOverride to the movement component.
                if (!_fpsMovement.SpeedOverrides.Contains(SpeedOverride)) {
                    _fpsMovement.SpeedOverrides.Add(SpeedOverride);
                }
            }
            else {
                // Try to remove the SpeedOverride from the movement component.
                if (_fpsMovement.SpeedOverrides.Contains(SpeedOverride)) {
                    _fpsMovement.SpeedOverrides.Remove(SpeedOverride);
                }
            }
        }

        float SpeedOverride() => _movementSpeed;
        #endregion
    }
}