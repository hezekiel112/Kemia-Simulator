using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KemiaSimulatorEnvironment.GameHandler {
    public class GameManager : MonoBehaviour {
        [Header("Scene System :")]
        public ScenePlayer ScenePlayer;

        [Header("Game Database :")]
        public KemiaSimulatorObjectDatabase ObjectDatabase;
        public KemiaSimulatorOrderDatabase OrderDatabase;

        [Header("Game Identity : (DevelopmentBuild value is took inside BuildSettings)")]
        [SerializeField] byte _majorVersion;
        [SerializeField] byte _minorVersion, _patchVersion;
        [SerializeField] int _revisionNumber;

        public GameBuildIdentity BuildIdentity {
            get; set;
        }

        public static GameManager Singleton {
            get; set;
        }

        public KemiaSimulatorObjectDatabase GetObjectDatabase() => ObjectDatabase;
        public KemiaSimulatorOrderDatabase GetOrderDatabase() => OrderDatabase;

        public void OnEnable() {
            Singleton = this;
            DontDestroyOnLoad(Singleton);

            BuildIdentity = new GameBuildIdentity(_majorVersion, _minorVersion, _patchVersion, _revisionNumber, Debug.isDebugBuild);
        }
    }
}