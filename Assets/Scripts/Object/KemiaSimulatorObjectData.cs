using System;
using UnityEngine;

namespace KemiaSimulatorEnvironment.Object {
    [Serializable]
    public struct KemiaSimulatorObjectData {
        [SerializeField] string _objectName;
        [SerializeField] KemiaSimulatorObjectType _objectType;
        public readonly string ObjectName {
            get {
                return _objectName;
            }
        }

        public readonly KemiaSimulatorObjectType ObjectType {
            get {
                return _objectType;
            }
        }
    }
}