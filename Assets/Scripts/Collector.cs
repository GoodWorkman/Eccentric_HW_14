using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class Collector : MonoBehaviour
{
    [SerializeField] private float _distanceToCollect;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private ExperienceManager _experienceManager;
    [SerializeField] private PlayerHealth _playerHealth;

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _distanceToCollect, _layerMask,
            QueryTriggerInteraction.Ignore);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponentInParent<Loot>() is Loot loot)
            {
                loot.Collect(this);
            }
        }
    }

    public void TakeExperience(int lootValue)
    {
        _experienceManager.AddExperience(lootValue);
    }
    
    public void TakeHealth(float lootValue)
    {
        _playerHealth.AddHealth(lootValue);
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, _distanceToCollect);
    }
#endif
}