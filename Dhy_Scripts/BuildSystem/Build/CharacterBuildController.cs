using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using SurvivalGames.Util;
public class CharacterBuildController : CharacterBehaviour, IBuildingController
{
    
    [SerializeField]
    protected GameObject[] m_Members;
    public BuildableDefinition Buildable { get; private set; }
    [SerializeField]
    private StructurePlacementState m_PlacementState;
    public float RotationOffset { get; set; }

    public ILookHandler m_LookHandler;
    public ICharacterAnimationController m_characterAnimationController;

    private float m_NextTimeCanPlace;

    public event UnityAction BuildingStarted
    {
        add => m_OnBuildingStart.AddListener(value);
        remove => m_OnBuildingStart.RemoveListener(value);
    }

    public event UnityAction BuildingStopped
    {
        add => m_OnBuildingEnd.AddListener(value);
        remove => m_OnBuildingEnd.RemoveListener(value);
    }

    public event UnityAction ObjectPlaced
    {
        add => m_OnPlaceObject.AddListener(value);
        remove => m_OnPlaceObject.RemoveListener(value);
    }
    [SerializeField]
    private UnityEvent m_OnPlaceObject;

    [SerializeField]
    private UnityEvent m_OnBuildingStart;

    [SerializeField]
    private UnityEvent m_OnBuildingEnd;

    [SerializeField]
    private InputContextGroup m_BuildingContext;

    public event UnityAction<BuildableDefinition> BuildableChanged;
    protected override void OnBehaviourEnabled()
    {
        //InitializeBuildStates();
        //Debug.Log("OnBehaviourEnabled()");
        m_PlacementState.Initialize(Character);
        GetModule(out m_LookHandler);
        GetModule(out m_characterAnimationController);
        //GetModule(out m_CameraEffects);
        //BuildableDefinition[] buildables = BuildableDefinition.GetAllBuildablesOfType<StructureBuildable>();
        //data = ItemManager.Instance.datas[2];
        //ΪUI��������
        /*foreach (var item in GridManager.Instance.weaponGrids)  //������Ӳ鿴�Ƿ��������
        {
            if (item.GetComponent<GridData>().data != null)  //�����ݵ����
            {
                if (item.GetComponent<GridData>().data == data)  //������ӵ�������Ķ�������ͬ
                {
                    item.GetComponent<GridData>().num += 1;
                    break;  //�Ķ����˳�
                }
                else  //�����ͬ
                {
                    continue;
                }
            }
            else  //û�����ݵ����
            {
                item.GetComponent<GridData>().data = data;
                GridManager.Instance.itemDatas.Add(item.GetComponent<GridData>().data);
                item.GetComponent<GridData>().num = 1;
                break;  //�������ݺ��˳�
            }
        }*/
    }
    /*private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            BuildableDefinition[] buildables = BuildableDefinition.GetAllBuildablesOfType<StructureBuildable>();
            SetBuildable(buildables[0]);
            DebugUtil.Log("���������");
        }
    }*/
    public void SetBuildable(BuildableDefinition buildable)
    {
        if (buildable == Buildable)
            return;
        DebugUtil.Log("SetBuildable");
        if (m_PlacementState.Buildable != null)
            Destroy(m_PlacementState.Buildable.gameObject);

        //�������������
        if (Buildable == null && buildable != null)
        {
            m_OnBuildingStart.Invoke();
            InputManager.PushContext(m_BuildingContext);
            InputManager.PushEscapeCallback(ForceEndBuilding);
            m_LookHandler.PostViewUpdate += UpdateObjectPlacement;
        }
        else if (Buildable != null && buildable == null)
        {
            m_OnBuildingEnd.Invoke();
            InputManager.PopContext(m_BuildingContext);
            InputManager.PopEscapeCallback(ForceEndBuilding);
            m_LookHandler.PostViewUpdate -= UpdateObjectPlacement;
        }

        Buildable = buildable;
        var floatingBuildable = CreateBuildable(buildable);
        m_PlacementState.TrySetBuildable(floatingBuildable);
        //m_PlacementState = GetPlacementStateForBuildable(buildable);
        void ForceEndBuilding() => SetBuildable(null);
        void UpdateObjectPlacement() => m_PlacementState.UpdatePlacement(RotationOffset);
    }
    public void PlaceBuildable()
    {
        if (Time.time < m_NextTimeCanPlace)
            return;

        if (m_PlacementState.TryPlaceActiveBuildable())
        {
            if (m_PlacementState.ContinueBuildingOnPlaced)
                m_PlacementState.TrySetBuildable(CreateBuildable(Buildable));
            else
            {
                m_PlacementState.TrySetBuildable(null);
                SetBuildable(null);
            }

            // Play place effects.
            DebugUtil.Log("Palce");
            m_characterAnimationController.BeginEventing(EventDefinitionType.Hammer);
            m_OnPlaceObject.Invoke();
        }
        else
        {
            // Play invalid place sound.
            m_NextTimeCanPlace = Time.time + 0.5f;
        }
    }
    private static Buildable CreateBuildable(BuildableDefinition definition)
    {
        if (definition == null)
            return null;
        //Debug.Log("CreateBuildable");
        var floatingBuildable = Instantiate(definition.Prefab);
        floatingBuildable.Definition = definition;
        floatingBuildable.OnCreated();

        return floatingBuildable;
    }
}
