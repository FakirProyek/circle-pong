using System;
using System.Collections.Generic;
using UnityEngine;
using BlitheFramework;
using Photon.Pun;

public class FactoryBat: BaseClass
{
    #region EVENT
    public event EventHandler EVENT_REMOVE;
    #endregion EVENT
    private List<Bat> listOfObjetFactories;
    public FactoryBat()
    {
        listOfObjetFactories = new List<Bat>();
        Init();
    }
    public override void Init()
    {

    }

    public void Add(GameObject _object, Vector3 _position, Quaternion _rotation, bool _isHost, float _angle)
    {
        Bat bat = new Bat();
        bat = PhotonNetwork.Instantiate(_object.name, _position, _rotation).AddComponent<Bat>() as Bat;
        bat.Init(_isHost, _angle);
        #region EVENT_LISTENER_ADD_Bat
        bat.GetComponent<Bat>().EVENT_REMOVE += Remove;
        #endregion EVENT_LISTENER_ADD_Bat
        listOfObjetFactories.Add(bat);
    }
    public Bat Get(int _indexObjectOnList)
    {
        return (listOfObjetFactories[_indexObjectOnList]) as Bat;
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
        listOfObjetFactories.Remove(sender.GetComponent<Bat>());
        #region EVENT_LISTENER_REMOVE_Bat
        sender.GetComponent<Bat>().EVENT_REMOVE -= Remove;
        #endregion EVENT_LISTENER_REMOVE_Bat
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