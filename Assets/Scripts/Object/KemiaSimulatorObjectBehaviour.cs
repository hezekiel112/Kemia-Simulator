using UnityEngine;
using UnityEngine.Events;

namespace KemiaSimulatorEnvironment.Object {
    /// <summary>
    /// base behaviour for all KemiaSimulator's Object
    /// </summary>
    public abstract class KemiaSimulatorObjectBehaviour : MonoBehaviour {
        public abstract KemiaSimulatorObject Instance {
            get;
        }

        public abstract UnityEvent OnTriggerInvoke {
            get;
        }

        /// <summary>
        /// called with unity OnEnable
        /// </summary>
        public virtual void OnObjectSpawn() {}

        /// <summary>
        /// called with unity OnDestroy
        /// </summary>
        public virtual void OnObjectDestroy() {}

        private void OnDestroy() {
            OnObjectDestroy();
        }

        private void OnEnable() {
            OnObjectSpawn();
        }
    }
}
