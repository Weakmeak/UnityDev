using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionEvent))]
public class Damage : Interactable
{
    [SerializeField] float damage = 0;
    [SerializeField] bool oneTime = true;

    void Start()
    {
        GetComponent<CollisionEvent>().onEnter += OnInteract;
        if (!oneTime) GetComponent<CollisionEvent>().onStay += OnInteract;

    }

    public override void OnInteract(GameObject target)
    {
        if (target.TryGetComponent<PlayerHealth>(out PlayerHealth health))
        {
            health.OnApplyDamage(damage * ((oneTime) ? 1 : Time.deltaTime));
            Vector3 temp = target.transform.position;
            temp.y = 0.5f;
            Instantiate(interactFX, temp, Quaternion.identity);
        }
    }
}
