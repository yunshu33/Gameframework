using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class DialogueNodeBase : Node,ISetData<NodeScriptableObjectBase> 
{

    public string guid;

    public Vector2 position;

    protected List<Port> ports = new List<Port>();

    private NodeScriptableObjectBase scriptableObject;

    public NodeScriptableObjectBase Data { get => scriptableObject; set => scriptableObject = value; }

    protected override void OnPortRemoved(Port port)
    {
        ports.Remove(port);
       
        base.OnPortRemoved(port);
    }

    public DialogueNodeBase()
    {
        CreateInputPort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single,null);
        CreateOutputPort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, null);

        RefreshExpandedState();
        RefreshPorts();


        if (Data is null)
        {
            guid = Guid.NewGuid().ToString();
        }else
        {
            guid= Data.guid;
        }

    }
    public  DialogueNodeBase(NodeScriptableObjectBase scriptableObject) : this()
    {
        Data = scriptableObject;
    }


    public abstract Port CreateInputPort(Orientation orientation, Direction direction, Port.Capacity capacity, Type type);

    public abstract Port CreateOutputPort(Orientation orientation, Direction direction, Port.Capacity capacity, Type type);

    public override Port InstantiatePort(Orientation orientation, Direction direction, Port.Capacity capacity, Type type)
    {
        var port = DialoguePort.Create<Edge>(orientation, direction, capacity, type);

        ports.Add(port);

        return port;
    }


    public abstract NodeScriptableObjectBase SaveData();
}
