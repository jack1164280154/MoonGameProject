using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryHandler : CharacterBehaviour, IInventoryHandler
{
    [SerializeField]
    InputContextGroup m_InventoryUIContext;
    public bool InspectionActive { get => m_InspectionActive; set => m_InspectionActive = value; }

    private bool m_InspectionActive;

    private IBuildingController m_BuildingController;
    private ICharacterAnimationController m_CharacterAnimationController;
    private ICharacterUIHandler m_characterUIHandler;
    private IPostProcess m_postProcess;
    private GameObject uiView;
    //private IUIController m_UICo
    protected override void OnBehaviourEnabled()
    {
        GetModule(out m_BuildingController);
        GetModule(out m_CharacterAnimationController);
        GetModule(out m_characterUIHandler);
        GetModule(out m_postProcess);
        GameObject uiview = Resources.Load<GameObject>("UIViews/backpack");
        uiView = Instantiate(uiview, m_characterUIHandler.transform);
        uiView.SetActive(false);
        foreach (var item in ItemManager.Instance.datas)
        {
            TestCallAddItem.Instance.AddItem(item, 10);
        }
    }
    public bool TryStartInspection()
    {
        m_CharacterAnimationController.BeginOpenBackPack();
        if (uiView == null)
        {
            GameObject uiview = Resources.Load<GameObject>("UIViews/backpack");
            uiView = Instantiate(uiview, m_characterUIHandler.transform);
        }
        else
        {
            uiView.SetActive(true);

        }
        //关闭主角UI
        m_characterUIHandler.CloseView();
        m_InspectionActive = true;
        //背景模糊
        m_postProcess.BeginGlobalBlur();
        CursorLocker.AddCursorUnlocker(this);
        InputManager.PushEscapeCallback(ForceEndInspection);
        InputManager.PushContext(m_InventoryUIContext);
        return true;
    }
    public bool TryStopInspection(BuildableDefinition buildableDef)
    {
        /*if (Time.time < m_NextTimeCanToggle || !InputManager.HasEscapeCallbacks)
            return false;*/
        uiView.SetActive(false);
        //角色动画进入正常状态
        m_CharacterAnimationController.EndBackPack();
        //关闭模糊后处理效果
        m_postProcess.CloseGlobalBlur();
        if (buildableDef == null)
        {
            ForceEndInspection();
        }
        else
        {
            StopInspection();
            m_BuildingController.SetBuildable(buildableDef);
        }
        //打开主角ui
        m_characterUIHandler.CreateOrOpenView();
        return true;
    }
    private void ForceEndInspection()
    {
        //m_SelectionHandler.SelectAtIndex(m_SelectionHandler.SelectedIndex);
        StopInspection();
        //print("ForceEndInspection()");
    }
    private void StopInspection()
    {
        m_InspectionActive = false;

        //CursorLocker.RemoveCursorUnlocker(this);
        InputManager.PopEscapeCallback(ForceEndInspection);
        InputManager.PopContext(m_InventoryUIContext);
    }
}