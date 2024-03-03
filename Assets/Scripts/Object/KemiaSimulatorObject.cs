using UnityEngine;
using UnityEngine.Events;

namespace KemiaSimulatorEnvironment.Object {
    public sealed class KemiaSimulatorObject : KemiaSimulatorObjectBehaviour {
        [Header("World Identity :")]
        public KemiaSimulatorObjectSO Data;

        public override KemiaSimulatorObject Instance => this;

        [SerializeField] UnityEvent _onTriggerInvoke = new();

        public override UnityEvent OnTriggerInvoke => _onTriggerInvoke;

        /// <summary>
        /// the value is assigned using the transform's GetHashCode when OnObjectSpawn is called
        /// </summary>
        public int HashValue {
            get; set;
        }

        public override void OnObjectSpawn() {
            HashValue = transform.GetInstanceID();
        }
    }
}