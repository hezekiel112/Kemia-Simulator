using UnityEngine;

namespace KemiaSimulatorEnvironment.Object {
    public class KemiaSimulatorObjectInvokeMachine {

        public readonly KemiaSimulatorObject ReferencedObject;

        public KemiaSimulatorObjectInvokeMachine(KemiaSimulatorObject referencedObject) {
            ReferencedObject = referencedObject;
        }
    }
}