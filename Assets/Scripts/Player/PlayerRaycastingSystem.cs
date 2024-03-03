using System;
using System.Collections;
using System.Collections.Generic;
using KemiaSimulatorEnvironment.GameHandler;
using KemiaSimulatorEnvironment.Object;
using UnityEngine;
using static UnityEditor.Searcher.Searcher.AnalyticsEvent;

public class PlayerRaycastingSystem : MonoBehaviour
{
    [Header("Raycast Settings :")]
    [SerializeField] LayerMask _hitMask;

    public static event Action<KemiaSimulatorObject> OnRaycastStart;

    private void OnEnable() {
        OnRaycastStart += InvokeCastedObjectWithEvent;
    }

    private void FixedUpdate() {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _hitMask)) {
            if (Input.GetKeyDown(KeyCode.F)) {
                OnRaycastStart(GameManager.Singleton.GetObjectDatabase().Get(hit.transform.GetInstanceID()));
            }
        }
    }

    public void InvokeCastedObjectWithEvent(KemiaSimulatorObject obj) {
        obj.OnTriggerInvoke.Invoke();
    }
}