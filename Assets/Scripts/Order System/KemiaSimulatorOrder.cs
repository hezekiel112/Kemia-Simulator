using System;
using KemiaSimulatorEnvironment.Object;

[Serializable]
public class KemiaSimulatorOrder {
    public KemiaSimulatorOrderSO Order;

    public string GetOrderName() => Order.KsOrderProfile.OrderName;
    public int GetFoodOrDrinkCount() => Order.KsOrderProfile.FoodOrDrinkCount;
    public KemiaSimulatorObjectSO GetFoodOrDrinkObjectSO() => Order.KsOrderProfile.FoodOrDrinkObject;
}