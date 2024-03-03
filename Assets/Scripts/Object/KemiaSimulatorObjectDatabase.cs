using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using KemiaSimulatorEnvironment.Object;
using UnityEngine;

public class KemiaSimulatorObjectDatabase : MonoBehaviour
{
    [Header("Objects :")]
    [SerializeField] KemiaSimulatorObject[] _ksObjects;

    Dictionary<int, KemiaSimulatorObject> _ksObjectsDictionnary = new();

    public ReadOnlyDictionary<int, KemiaSimulatorObject> KSObjectsDictionnary {
        get {
            return new(_ksObjectsDictionnary);
        }
    }

    private void OnEnable() {
        for (int i = 0; i < _ksObjects.Length; i++) {
            Add(_ksObjects[i].HashValue, _ksObjects[i]);
        }
    }

    public void Add(int hash, KemiaSimulatorObject ksObject) => _ksObjectsDictionnary.Add(hash, ksObject);
    public KemiaSimulatorObject Get(int hash) => KSObjectsDictionnary[hash];
}