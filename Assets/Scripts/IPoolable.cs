using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    void SetPoolObjectReleaseAction(Action<IPoolable> action);
    void ResetObjectStateToDefaults();
    void SetActive(bool active);
    void Destroy();
}
