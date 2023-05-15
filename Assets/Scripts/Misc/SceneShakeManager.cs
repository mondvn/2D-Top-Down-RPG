using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SceneShakeManager : Singleton<SceneShakeManager>
{
    private CinemachineImpulseSource source;

    protected override void Awake()
    {
        base.Awake();
        this.source = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeScreen()
    {
        source.GenerateImpulse();
    }
}
