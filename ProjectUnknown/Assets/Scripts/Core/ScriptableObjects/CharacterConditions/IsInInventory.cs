using UnityEngine;

[CreateAssetMenu(fileName = "IsInInventory", menuName = "BehaviorCondition/IsInInventory")]
public class IsInInventory : CharacterBehaviorCondition
{
    [SerializeField]
    bool reverse = false;
    public override bool IsSatisfied(Character character)
    {
        var inventorySystem = InventorySystem.GetInstance();
        if (inventorySystem == null)
        {
            return reverse ? true : false;
        }
        else
        {
            return reverse ? !inventorySystem.IsOpen() : inventorySystem.IsOpen();
        }
    }
}
