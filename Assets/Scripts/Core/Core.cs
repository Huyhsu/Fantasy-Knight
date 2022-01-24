using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    private List<ILogicUpdate> _components = new List<ILogicUpdate>();

    public void LogicUpdate()
    {
        foreach (var component in _components)
        {
            component.LogicUpdate();
        }
    }

    public void AddComponent(ILogicUpdate component)
    {
        if (!_components.Contains(component))
        {
            _components.Add(component);
        }
    }
}
