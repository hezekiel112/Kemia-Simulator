﻿using UnityEngine;

namespace KemiaSimulatorEnvironment.Player {
    public class Jump : MonoBehaviour {
        Rigidbody _rb;
        public float jumpStrength = 2;
        public event System.Action Jumped;

        [SerializeField, Tooltip("Prevents jumping when the transform is in mid-air.")]
        GroundCheck groundCheck;


        void Reset() {
            // Try to get groundCheck.
            groundCheck = GetComponentInChildren<GroundCheck>();
        }

        void Awake() {
            // Get rigidbody.
            _rb = GetComponent<Rigidbody>();
        }

        void LateUpdate() {
            // Jump when the Jump button is pressed and we are on the ground.
            if (Input.GetButtonDown("Jump") && (!groundCheck || groundCheck.isGrounded)) {
                _rb.AddForce(Vector3.up * 100 * jumpStrength);
                Jumped?.Invoke();
            }
        }
    }
}