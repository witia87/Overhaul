using UnityEngine;

public class BaseDangerNode
{
    private readonly Vector3 _position;

    public BaseDangerNode(Vector3 position)
    {
        _position = position;
    }

    public int DangersCount { get; set; }

    public bool IsDangerous
    {
        get { return DangersCount > 0; }
    }
}