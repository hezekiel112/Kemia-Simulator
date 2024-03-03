using System.Collections.Generic;
using UnityEngine;

namespace KemiaSimulatorEnvironment.GameHandler {
    [CreateAssetMenu()]
    public class GameSceneDatabase : ScriptableObject {
        [SerializeField] GameScene[] _gameScenes;
        public GameScene[] GameScenes => _gameScenes;

        public IEnumerable<GameScene> Childrens => GameScenes;
        public IEnumerable<GameScene> GetChildrens() {
            return Childrens;
        }
    }
}