using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KemiaSimulatorEnvironment.GameHandler {
    public class ScenePlayer : MonoBehaviour {
        [Header("Scenes Registry :")]
        public GameSceneDatabase GameSceneDatabase;

        public Dictionary<GameScene, Scene> GameScenes = new Dictionary<GameScene, Scene>();

        public void InitalizeScenesDatabase() {
            try {
                for (int i = 0; i < GameSceneDatabase.GameScenes.Length; i++) {
                    GameScenes.Add(GameSceneDatabase.GameScenes[i],
                        SceneManager.GetSceneAt(GameSceneDatabase.GameScenes[i].UNITY_ID));
                }
            }
            catch(Exception ex) {
                Debug.LogException(ex);
                throw;
            }
        }

        public void LoadScene(int gameSceneIndex) {
            SceneManager.LoadScene(GameSceneDatabase.GameScenes[gameSceneIndex].UNITY_ID);
        }

        public Scene GetNativeSceneFromKS(GameScene scene) {
            return GameScenes[scene];
        }
    }
}