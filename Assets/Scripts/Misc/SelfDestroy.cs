using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

    private void Awake()
    {
        this.ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (this.ps && !this.ps.IsAlive()) DestroySelfAnimEvent();
    }

    public void DestroySelfAnimEvent()
    {
        if (gameObject != null) Destroy(gameObject);
    }
}
