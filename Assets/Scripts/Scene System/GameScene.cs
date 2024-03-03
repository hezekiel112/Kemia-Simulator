using System;
using System.Runtime.InteropServices;
using UnityEngine.UI;
using UnityEngine;

namespace KemiaSimulatorEnvironment.GameHandler {
    [Serializable]
    public record GameScene {

        [SerializeField] string _sceneName;
        [SerializeField] string _sceneDescription;
        [SerializeField] Image _sceneIcon;
        [SerializeField] int _unitySceneID;

        /// <summary>
        /// it must not be sampled with <see cref="GAME_ID"/> as this variable is for being linked with the original Unity's scene.        
        /// </summary>
        public int UNITY_ID {
            get;
        }
        /// <summary>
        /// kemia simulator's scene name identificator.
        /// </summary>
        public string GAME_ID {
            get;
        }

        /// <summary>
        /// this field is showed in-game.
        /// </summary>
        public string GAME_DESCRIPTION {
            get;
        }

        /// <summary>
        /// this image is showed in-game.
        /// </summary>
        public Image GAME_ICON {
            get;
        }

        public string GetSceneName() => GAME_ID;
        public string GetSceneDescription() => GAME_DESCRIPTION;
        public int GetUnitySceneID() => UNITY_ID;
        public Image GetSceneIcon() => GAME_ICON;

        public GameScene() {
            UNITY_ID = _unitySceneID;
            GAME_ID = _sceneName;
            GAME_DESCRIPTION = _sceneDescription;
            GAME_ICON = _sceneIcon;
        }
    }
}