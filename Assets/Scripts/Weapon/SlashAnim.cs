using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAnim : MonoBehaviour
{
    public void DestroySelf()
    {
        if (gameObject != null) Destroy(gameObject);
    }
}
