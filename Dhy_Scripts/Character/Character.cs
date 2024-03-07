using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
public class Character : MonoBehaviour, ICharacter
{
    public bool IsInitialized { get; private set; }

    public Transform ViewTransform => m_View;
    public Collider[] Colliders { get; private set; }

    public event UnityAction Initialized;

    [SerializeField]
    [Tooltip("The view transform, you can think of it as the eyes of the character")]
    private Transform m_View;

    private Dictionary<Type, ICharacterModule> m_ModulesByType;
    private static readonly List<ICharacterModule> s_CachedModules = new(32);

    public bool TryGetModule<T>(out T module) where T : class, ICharacterModule
    {
        if (m_ModulesByType != null && m_ModulesByType.TryGetValue(typeof(T), out ICharacterModule charModule))
        {
            module = (T)charModule;
            return true;
        }
        else
        {
            module = default;
            return false;
        }
    }

    public void GetModule<T>(out T module) where T : class, ICharacterModule
    {
        if (m_ModulesByType != null && m_ModulesByType.TryGetValue(typeof(T), out ICharacterModule charModule))
        {
            module = (T)charModule;
            return;
        }

        module = default;
    }
    public T GetModule<T>() where T : class, ICharacterModule
    {
        if (m_ModulesByType != null && m_ModulesByType.TryGetValue(typeof(T), out ICharacterModule charModule))
            return (T)charModule;

        return default;
    }

    protected virtual void Awake()
    {
        SetupModules();
        SetupBaseReferences();
    }
    protected virtual void Start()
    {
        IsInitialized = true;
        Initialized?.Invoke();
    }
    protected virtual void SetupBaseReferences()
    {
        /*AudioPlayer = GetModule<IAudioPlayer>();
        HealthManager = GetModule<IHealthManager>();
        Inventory = GetModule<IInventory>();

        Colliders = GetComponentsInChildren<Collider>(true);*/
    }
    

    private void SetupModules()
    {
        // Find & Setup all of the Modules
        GetComponentsInChildren(s_CachedModules);
        for (int i = 0; i < s_CachedModules.Count; i++)
        {
            ICharacterModule module = s_CachedModules[i];

            Type[] interfaces = module.GetType().GetInterfaces();
            foreach (Type interfaceType in interfaces)
            {
                if (interfaceType.GetInterface(typeof(ICharacterModule).Name) != null)
                {
                    if (m_ModulesByType == null)
                        m_ModulesByType = new Dictionary<Type, ICharacterModule>();

                    if (!m_ModulesByType.ContainsKey(interfaceType))
                        m_ModulesByType.Add(interfaceType, module);
                    //else
                    //    Debug.LogError($"2 Modules of the same type ({module.GetType()}) found under {gameObject.name}.");
                }
            }
        }
    }
#if UNITY_EDITOR
    protected virtual void Reset()
    {
        gameObject.layer = LayerMask.NameToLayer("Character");
    }
#endif
}
