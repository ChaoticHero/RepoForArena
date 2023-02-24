using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1
public interface IManager
{
    // 2
    string State { get; set; }

    // 3
    void Initialize();
}
public abstract class BaseManager
{
    // 2
    protected string _state;
    public abstract string state { get; set; }

    // 3
    public abstract void Initialize();
}
public class CombatManager : BaseManager
{
    // 2
    public override string state
    {
        get { return _state; }
        set { _state = value; }
    }

    // 3
    public override void Initialize()
    {
        _state = "Manager initialized..";
        Debug.Log(_state);
    }

}