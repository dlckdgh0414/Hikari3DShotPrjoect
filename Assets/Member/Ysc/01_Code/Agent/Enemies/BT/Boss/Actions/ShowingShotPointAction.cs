using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ShowingShotPoint", story: "Shwoing object [Object] is [Second]", category: "Action", id: "b3d10b0d2e3eb8738ea7a7db1ddb58d6")]
public partial class ShowingShotPointAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Object;
    [SerializeReference] public BlackboardVariable<float> Second;

    private float _currentTime;
    
    protected override Status OnStart()
    {
        Object.Value.SetActive(true);

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _currentTime += Time.deltaTime;
        
        if (_currentTime > Second.Value)
        {
            _currentTime = 0;
            Object.Value.SetActive(false);
            return Status.Success;
        }

        return Status.Running;
    }
}

