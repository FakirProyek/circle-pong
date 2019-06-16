using System;
using System.Collections.Generic;
using UnityEngine;
using BlitheFramework;

public class FactoryBall: BaseClass
{
    #region EVENT
    public event EventHandler EVENT_REMOVE;
    #endregion EVENT
    private List<Ball> listOfObjetFactories;
    public FactoryBall()
    {
        listOfObjetFactories = new List<Ball>();
        Init();
    }
    public override void Init()
    {

    }

    public void Add(GameObject _object, Vector3 _position, Quaternion _rotation, Vector2 _velocity)
    {
        Ball ball = new Ball();
        ball = Instantiate(_object, _position, _rotation).AddComponent<Ball>() as Ball;
        ball.Velocity = _velocity;
        ball.Init();
        #region EVENT_LISTENER_ADD_Ball
        ball.GetComponent<Ball>().EVENT_REMOVE += Remove;
        #endregion EVENT_LISTENER_ADD_Ball
        listOfObjetFactories.Add(ball);
    }
    public Ball Get(int _indexObjectOnList)
    {
        return (listOfObjetFactories[_indexObjectOnList]) as Ball;
    }
    public void RemoveObjectFactories(int _indexObjectOnList)
    {
       Get(_indexObjectOnList).Remove();
    }
    public int GetNumberOfObjectFactories()
    {
       return listOfObjetFactories.Count;
    }
    #region EVENT_LISTENER_METHOD
    private void Remove(object _sender, EventArgs e)
    {
        GameObject sender = (GameObject)_sender;
        listOfObjetFactories.Remove(sender.GetComponent<Ball>());
        #region EVENT_LISTENER_REMOVE_Ball
        sender.GetComponent<Ball>().EVENT_REMOVE -= Remove;
        #endregion EVENT_LISTENER_REMOVE_Ball
        Destroy(sender);
    }
    #endregion EVENT_LISTENER_METHOD
    private void RemoveAllObjectFactories()
    {
        while (GetNumberOfObjectFactories() != 0)
        {
            RemoveObjectFactories(0);
        }
    }
    #region public method
    public void Remove()
    {
       RemoveAllObjectFactories();
       dispatchEvent(EVENT_REMOVE, this.gameObject, EventArgs.Empty);
    }
    #endregion
}