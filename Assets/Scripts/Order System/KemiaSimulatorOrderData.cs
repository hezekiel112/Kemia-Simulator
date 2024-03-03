using System;
using KemiaSimulatorEnvironment.Object;
using UnityEngine;

[Serializable]
public struct KemiaSimulatorOrderData {

    [SerializeField] string _orderName;
    [SerializeField] int _foodOrDrinkCount;
    [SerializeField] KemiaSimulatorObjectSO _foodOrDrinkObject;

    public readonly string OrderName => _orderName;
    public readonly int FoodOrDrinkCount => _foodOrDrinkCount;
    public readonly KemiaSimulatorObjectSO FoodOrDrinkObject => _foodOrDrinkObject;
}