using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateBehavior
{
    void Enter(Transform target);
    void Exit();
}
