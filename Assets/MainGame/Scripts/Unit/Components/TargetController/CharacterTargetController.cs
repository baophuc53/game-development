using Spine;
using Spine.Unity;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class CharacterTargetController : TargetController
{
    protected override Vector3 GetTargetPosition()
    {
        var mousePosition =  Input.mousePosition;
        var worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        return worldMousePosition;
    }
}