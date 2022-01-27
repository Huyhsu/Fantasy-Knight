using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionChecker : MonoBehaviour
{
    [SerializeField] private int maxHitsAllowed = 20;
    [SerializeField] private LayerMask contactLayerMask;

    private ContactFilter2D _contactFilter;
    private Collider2D _collider;
    
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;

        _contactFilter.useLayerMask = true;
        _contactFilter.layerMask = contactLayerMask;
    }

    public bool IsTouching
    {
        get
        {
            Collider2D[] hits = new Collider2D[maxHitsAllowed];
            return _collider.OverlapCollider(_contactFilter, hits) > 0;
        } 
    }
}
