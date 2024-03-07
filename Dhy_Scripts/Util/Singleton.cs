using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> //where T: new()
{
    private static Singleton<T> _instance;

    public Singleton<T> Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Singleton<T>();
            }
            return _instance;
        }
    }
}
