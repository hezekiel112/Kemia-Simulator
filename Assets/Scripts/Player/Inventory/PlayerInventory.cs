using System.Collections.Generic;
using KemiaSimulatorEnvironment.Object;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] List<KemiaSimulatorOrder> _ksOrdersList = new();

    public static PlayerInventory Instance {
        get; set;
    }

    private void OnEnable() {
        Instance = this;
    }

    public void AddOrder(KemiaSimulatorOrder order) {
        _ksOrdersList.Add(order);
    }

    public void MarkOrderAsComplete(int orderId) {
        _ksOrdersList.RemoveAt(orderId);
    }

    public void MarkOrderAsComplete(KemiaSimulatorOrder order) {
        _ksOrdersList.Remove(order);
    }
}
