using UnityEngine;

namespace KemiaSimulatorEnvironment.Player {
    [ExecuteInEditMode]
    public class Zoom : MonoBehaviour {
        public float defaultFOV = 60;
        public float maxZoomFOV = 15;
        [Range(0, 1)]
        public float currentZoom;
        public float sensitivity = 1;

        public Camera PlayerCamera => Camera.main;

        void Awake() {
            defaultFOV = PlayerCamera.fieldOfView;
        }

        void Update() {
            // Update the currentZoom and the camera's fieldOfView.
            currentZoom += Input.mouseScrollDelta.y * sensitivity * .05f;
            currentZoom = Mathf.Clamp01(currentZoom);
            PlayerCamera.fieldOfView = Mathf.Lerp(defaultFOV, maxZoomFOV, currentZoom);
        }
    }
}