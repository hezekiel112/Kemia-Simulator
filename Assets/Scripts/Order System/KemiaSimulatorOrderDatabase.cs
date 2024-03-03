using System.Collections.Generic;
using UnityEngine;

public class KemiaSimulatorOrderDatabase : MonoBehaviour {
    public KemiaSimulatorOrder[] CustomerOrders;

    public IEnumerable<KemiaSimulatorOrder> Childrens;

    public IEnumerable<KemiaSimulatorOrder> GetChildrens() {
        return Childrens;
    }

    public void AddRandomOrderToInventory() {
        PlayerInventory.Instance.AddOrder(CustomerOrders[Random.Range(0, CustomerOrders.Length)]);
    }
}