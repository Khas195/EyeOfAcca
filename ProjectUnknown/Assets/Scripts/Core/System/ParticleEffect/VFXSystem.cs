using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class VFXSystem : SingletonMonobehavior<VFXSystem>
{
    [SerializeField]
    [Required]
    VFXResources resourcesPack = null;
    protected override void Awake()
    {
        base.Awake();
    }
    public GameObject PlayEffect(VFXResources.VFXList vFX, Vector3 position, Quaternion rotation)
    {
        for (int i = 0; i < resourcesPack.resourcesList.Count; ++i)
        {
            var item = resourcesPack.resourcesList[i];
            if (item.tag == vFX)
            {

                if (item.prefab == null)
                {
                    return null;
                }
                return GameObject.Instantiate(item.prefab, position, rotation, this.transform);
            }
        }
        return null;
    }

}
