using UnityEngine;


namespace KemiaSimulatorEnvironment.Player {
    public class FirstPersonLook : MonoBehaviour {
        [SerializeField] Transform _character;
        [SerializeField] Vector2 _velocity;
        [SerializeField] Vector2 _frameVelocity;

        readonly float Sensitivity = 2;
        readonly float Smoothing = 1.5f;

        void OnEnable() {
            SetLockState(CursorLockMode.Confined);
        }

        static void SetLockState(CursorLockMode lockMode) => Cursor.lockState = lockMode;

        void Update() {
            // Get smooth velocity.
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * Sensitivity);
            _frameVelocity = Vector2.Lerp(_frameVelocity, rawFrameVelocity, 1 / Smoothing);
            _velocity += _frameVelocity;
            _velocity.y = Mathf.Clamp(_velocity.y, -90, 90);

            // Rotate camera up-down and controller left-right from velocity.
            transform.localRotation = Quaternion.AngleAxis(-_velocity.y, Vector3.right);
            _character.localRotation = Quaternion.AngleAxis(_velocity.x, Vector3.up);
        }
    }
}