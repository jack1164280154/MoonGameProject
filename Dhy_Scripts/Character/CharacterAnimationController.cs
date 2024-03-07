using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MxM;
using MxMGameplay;
using SurvivalGames.Util;

public class CharacterAnimationController : CharacterBehaviour,ICharacterAnimationController
{
    public enum EState
    {
        General,
        Sliding,
        Jumping,
        Construction,
        UIing,
        InspectingBackpack,
        OpenBackpack,
        Rifling,
        Eventing
    }
    public void TestOnPoseChange(MxMAnimator.PoseChangeData a_poseChangeData)
    {
        Debug.Log("Pose Id: " + a_poseChangeData.PoseId.ToString() + " Speed Mod: "
                  + a_poseChangeData.SpeedMod.ToString() + " Time Offset: " + a_poseChangeData.TimeOffset.ToString());
    }
    public MxMAnimator mxMAnimator => m_mxmAnimator;
    private MxMAnimator m_mxmAnimator;
    //private MxMBlendSpaceLayers m_blendSpaceLayers;
    private MxMTrajectoryGenerator m_trajectoryGenerator;
    private MxMRootMotionApplicator m_rootMotionAplicator;
    private LocomotionSpeedRamp m_locomotionSpeedRamp;
    private VaultDetector m_vaultDetector;

    private GenericControllerWrapper m_controller;

    [SerializeField]
    private MxMEventDefinition m_slideDefinition = null;

    [SerializeField]
    private MxMEventDefinition m_openDefinition = null;

    [SerializeField]
    private MxMEventDefinition m_jumpDefinition = null;

    [SerializeField]
    private MxMEventDefinition m_danceDefinition = null;
    [SerializeField]
    private MxMEventDefinition m_fireDefinition = null;
    [SerializeField]
    private MxMEventDefinition m_pickUpDefinition = null;
    [SerializeField]
    private MxMEventDefinition m_hammerDefinition = null;
    [SerializeField]
    private MxMEventDefinition m_itemChangeDefinition = null;

    [Header("Input Profiles")]
    [SerializeField]
    private MxMInputProfile m_generalLocomotion = null;

    [SerializeField]
    private MxMInputProfile m_strafeLocomotion = null;

    [SerializeField]
    private MxMInputProfile m_sprintLocomotion = null;

    public EState curState
    {
        get => m_curState;
        set => m_curState = value;
    }
    private EState m_curState = EState.General;

    private EState preState;
    private Vector3 m_lastPosition;
    private Vector3 m_curVelocity;

    private bool m_controllerOn;
    private float m_defaultControllerHeight;
    private float m_defaultControllerCenter;

    private bool bIsDancing = false;

#if Enable_Debug
    public Vector3 event_point;
#endif

    private ILookHandler m_lookHandler;
    private IWieldableHandler m_wieldableHandler;
    protected override void OnBehaviourEnabled()
    {
        GetModule(out m_lookHandler);
        GetModule(out m_wieldableHandler);
    }
    void Start()
    {
        m_mxmAnimator = GetComponentInChildren<MxMAnimator>();
        m_controller = GetComponent<GenericControllerWrapper>();
        m_trajectoryGenerator = GetComponentInChildren<MxMTrajectoryGenerator>();
        m_rootMotionAplicator = GetComponent<MxMRootMotionApplicator>();
        m_vaultDetector = GetComponent<VaultDetector>();
        m_locomotionSpeedRamp = GetComponent<LocomotionSpeedRamp>();

        // m_blendSpaceLayers = GetComponent<MxMBlendSpaceLayers>();
        //m_mxmAnimator.BlendInLayer(m_blendSpaceLayers.LayerId, 4, 1f);
        m_mxmAnimator.AddRequiredTag("Strafe");
        m_mxmAnimator.SetCalibrationData("Strafe");
        m_mxmAnimator.SetFavourCurrentPose(true, 0.95f);
        m_locomotionSpeedRamp.ResetFromSprint();
        m_mxmAnimator.AngularErrorWarpRate = 360f;
        m_mxmAnimator.AngularErrorWarpThreshold = 270f;
        m_mxmAnimator.AngularErrorWarpMethod = EAngularErrorWarpMethod.TrajectoryFacing;
        m_trajectoryGenerator.TrajectoryMode = ETrajectoryMoveMode.Strafe;
        m_trajectoryGenerator.InputProfile = m_strafeLocomotion;
        m_mxmAnimator.PastTrajectoryMode = EPastTrajectoryMode.CopyFromCurrentPose;


        m_defaultControllerHeight = m_controller.Height;
        m_defaultControllerCenter = m_controller.Center.y;

        m_controller.Initialize();

        m_trajectoryGenerator.InputProfile = m_generalLocomotion;
    }
    void Update()
    {
        if (m_locomotionSpeedRamp != null)
            m_locomotionSpeedRamp.UpdateSpeedRamp();

        Vector3 position = transform.position;
        m_curVelocity = (position - m_lastPosition) / Time.deltaTime;
        m_curVelocity.y = 0f;

        switch (m_curState)
        {
            case EState.General:
                {
                    UpdateGeneral();
                }
                break;
            case EState.Sliding:
                {
                    UpdateSliding();
                }
                break;
            case EState.Jumping:
                {
                    UpdateJump();
                }
                break;
            case EState.UIing:
                {
                    UpdateUIing();
                }
                break;
            case EState.Rifling:
                {
                    UpdateRifling();
                }
                break;
            case EState.Eventing:
                {
                    UpdateEventing();
                }
                break;
        }

        m_lastPosition = position;
    }

    private void UpdateGeneral()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (bIsDancing)
            {
                m_mxmAnimator.EndLoopEvent();
                bIsDancing = false;

            }
            else
            {
                m_mxmAnimator.BeginEvent(m_danceDefinition);
                bIsDancing = true;
            }
        }

        if (bIsDancing)
            return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            BeginRifling();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            m_locomotionSpeedRamp.BeginSprint();
            m_trajectoryGenerator.MaxSpeed = 6.7f;
            m_trajectoryGenerator.PositionBias = 6f;
            m_trajectoryGenerator.DirectionBias = 6f;
            m_mxmAnimator.SetCalibrationData("Sprint");
            m_trajectoryGenerator.InputProfile = m_sprintLocomotion;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_locomotionSpeedRamp.ResetFromSprint();
            m_trajectoryGenerator.MaxSpeed = 4.3f;
            m_trajectoryGenerator.PositionBias = 10f;
            m_trajectoryGenerator.DirectionBias = 10f;
            m_mxmAnimator.SetCalibrationData("General");
            m_trajectoryGenerator.InputProfile = m_generalLocomotion;
        }

        //Loop Blend Test
        if (Input.GetKeyDown(KeyCode.L))
        {
            m_mxmAnimator.BeginLoopBlend(1, 0f, 0f);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            m_mxmAnimator.SetBlendSpacePositionX(Mathf.Clamp(m_mxmAnimator.DesiredBlendSpacePositionX + 0.1f, -1f, 1f));
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            m_mxmAnimator.SetBlendSpacePositionX(Mathf.Clamp(m_mxmAnimator.DesiredBlendSpacePositionX - 0.1f, -1f, 1f));
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            m_mxmAnimator.BeginEvent(m_slideDefinition);
            BeginSliding();

        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_jumpDefinition != null)
            {
                m_jumpDefinition.ClearContacts();
                m_jumpDefinition.AddDummyContacts(1);
                m_mxmAnimator.BeginEvent(m_jumpDefinition);

                ref readonly EventContact eventContact = ref m_mxmAnimator.NextEventContactRoot_Actual_World;

                Ray ray = new Ray(eventContact.Position + (Vector3.up * 3.5f), Vector3.down);
                RaycastHit rayHit = new RaycastHit();

                if (Physics.Raycast(ray, out rayHit, 10f, 1 << 6)
                    && rayHit.distance > 1.5f
                    && rayHit.distance < 5f)
                {
                    event_point = rayHit.point;
                    m_mxmAnimator.ModifyDesiredEventContactPosition(rayHit.point);
                }
                else
                {
                    event_point = eventContact.Position;
                    m_mxmAnimator.ModifyDesiredEventContactPosition(eventContact.Position);
                }

                m_controller.enabled = false;
                m_rootMotionAplicator.EnableGravity = false;

                m_curState = EState.Jumping;
            }
        }
    }
#if Enable_Debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireCube(event_point, Vector3.one);
        Gizmos.DrawSphere(event_point, .5f);
    }
#endif

    /*public void EnableGravityFunction()
    {
        Vector3 moveDelta = Vector3.zero;
        if (m_controller.enabled && m_rootMotionAplicator.EnableGravity && !m_controller.IsGrounded)
        {
            moveDelta.y = (m_controller.Velocity.y + Physics.gravity.y * Time.deltaTime) * Time.deltaTime;
        }
        m_rootMotionAplicator.ControllerWrapper.Move(moveDelta * Time.deltaTime);
    }*/
    public void BeginRifling()
    {

        curState = EState.Rifling;
        m_mxmAnimator.AddRequiredTag("Rifle");
        m_mxmAnimator.RemoveRequiredTag("Strafe");
        m_mxmAnimator.SetFavourCurrentPose(false, 1.0f);
        m_mxmAnimator.SetCalibrationData(0);
        m_mxmAnimator.AngularErrorWarpRate = 60.0f;
        m_mxmAnimator.AngularErrorWarpThreshold = 90f;
        m_mxmAnimator.AngularErrorWarpMethod = EAngularErrorWarpMethod.CurrentHeading;
        m_trajectoryGenerator.TrajectoryMode = ETrajectoryMoveMode.Normal;
        m_trajectoryGenerator.InputProfile = m_generalLocomotion;
        m_mxmAnimator.PastTrajectoryMode = EPastTrajectoryMode.ActualHistory;
        
        m_lookHandler.ChangeViewToFirstLook();
    }
    public void ExitRifling()
    {
        m_mxmAnimator.RemoveRequiredTag("Rifle");
        m_mxmAnimator.AddRequiredTag("Strafe");
        m_mxmAnimator.SetCalibrationData("Strafe");
        m_mxmAnimator.SetFavourCurrentPose(true, 0.95f);
        m_locomotionSpeedRamp.ResetFromSprint();
        m_mxmAnimator.AngularErrorWarpRate = 360f;
        m_mxmAnimator.AngularErrorWarpThreshold = 270f;
        m_mxmAnimator.AngularErrorWarpMethod = EAngularErrorWarpMethod.TrajectoryFacing;
        m_trajectoryGenerator.TrajectoryMode = ETrajectoryMoveMode.Strafe;
        m_trajectoryGenerator.InputProfile = m_strafeLocomotion;
        m_lookHandler.ChangeViewToThirdLook();
        curState = EState.General;
    }
    void UpdateRifling()
    {
        DebugUtil.Log("UpdateRifling");
        if (Input.GetMouseButtonDown(0) && m_mxmAnimator.IsEventComplete)
        {
            m_mxmAnimator.BeginEvent(m_fireDefinition);
            m_wieldableHandler.OnMouseButtonDown();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ExitRifling();
        }
    }

    public void BeginEventing(EventDefinitionType type)
    {
        preState = m_curState;
        switch (type)
        {
            case EventDefinitionType.PickUp:
                m_mxmAnimator.BeginEvent(m_pickUpDefinition);
                break;
            case EventDefinitionType.Hammer:
                m_mxmAnimator.BeginEvent(m_hammerDefinition);
                m_wieldableHandler.Hammer.SetActive(true);
                break;
            case EventDefinitionType.ItemChange:
                m_mxmAnimator.BeginEvent(m_itemChangeDefinition);
                break;
            default:
                break;
        }
        
        m_curState = EState.Eventing;
    }
    public void ExitEventing()
    {
        m_wieldableHandler.Hammer.SetActive(false);
        m_curState = preState;
    }
    void UpdateEventing()
    {
        if (m_mxmAnimator.IsEventComplete)
        {
            ExitEventing();
        }
    }
    public void BeginOpenBackPack()
    {
        m_mxmAnimator.BeginEvent(m_openDefinition);
        preState = curState;
        curState = EState.UIing;

    }
    private void BeginSliding()
    {
        m_curState = EState.Sliding;
        m_controller.Height = m_defaultControllerHeight / 2f;
        m_controller.Center = new Vector3(0f, m_defaultControllerCenter / 2f + 0.05f, 0f);
        //m_vaultDetector.enabled = false;
    }

    private void UpdateSliding()
    {
        if (m_mxmAnimator.IsEventComplete)
        {
            m_curState = EState.General;
            m_controller.Center = new Vector3(0f, m_defaultControllerCenter, 0f);
            m_controller.Height = m_defaultControllerHeight;
            //m_vaultDetector.enabled = true;
        }
    }

    private void UpdateJump()
    {
        if (m_mxmAnimator.IsEventComplete)
        {
            m_curState = EState.General;
            m_rootMotionAplicator.EnableGravity = true;
            m_controller.enabled = true;
            m_lastPosition = transform.position;
            m_curVelocity = Vector3.zero;
        }
    }
    private void UpdateUIing()
    {
        if (m_mxmAnimator.IsEventComplete)
        {
            m_mxmAnimator.Pause();
        }
    }

    public void EndBackPack()
    {
        //Debug.Log(this.name + "EndBackPack()");
        if (!m_mxmAnimator.IsEventComplete)
        {
            curState = preState;
        }
        if (m_mxmAnimator.IsPaused)
        {
            m_mxmAnimator.UnPause();
            curState = preState;
            //Debug.Log(m_mxmAnimator.IsPaused);
        }
    }
}
