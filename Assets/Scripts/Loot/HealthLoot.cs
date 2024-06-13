using UnityEngine;

public class HealthLoot : Loot
{
    [SerializeField] private float _lootValue = 10;
    
    public override void TakeItem(Collector collector)
    {
        base.TakeItem(collector);
        collector.TakeHealth(_lootValue);
    }
}
