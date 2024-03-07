using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class DataDefinition<T> : DataDefinitionBase where T : DataDefinition<T>
{
    public static T[] Definitions
    {
        get
        {
            if (s_Definitions == Array.Empty<T>())
                LoadDefinitions();

            return s_Definitions;
        }
    }

    private static T[] s_Definitions = Array.Empty<T>();

    private static void LoadDefinitions()
    {
        string path = "Definitions/" + typeof(T).Name.Replace("Definition", "");
        //Debug.Log(path);

        s_Definitions = Resources.LoadAll<T>(path + "s");
        //Debug.Log(s_Definitions.Length);
        if (s_Definitions != null && s_Definitions.Length > 0)
            return;

        path = path.Remove(path.Length - 1, 1) + "ies";
        s_Definitions = Resources.LoadAll<T>(path);

        if (s_Definitions != null && s_Definitions.Length > 0)
            return;

        s_Definitions = Resources.LoadAll<T>(path);

        if (s_Definitions != null && s_Definitions.Length > 0)
            return;

        s_Definitions = Array.Empty<T>();
    }
}
