using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class EnemyTargetController : TargetController
{
    private Vector3 offset = new Vector3(0, 3, 0);

    protected override Vector3 GetTargetPosition()
    {
        return GameController.Instance.Character.transform.position + offset;
    }
}