using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MxM;
//using MxMGameplay;

namespace CharacterGameplay
{
    public class CharacterInput : MonoBehaviour
    {
        private MxMAnimator mxmAnimator;
        private MxMRootMotionApplicator rootMotionAplicator;
        private MxMTrajectoryGenerator trajectoryGenerator;
        private LocomotionSpeedRamp locomotionSpeedRamp;
        [SerializeField]
        private MxMEventDefinition jumpDefinition = null;
        [SerializeField]
        private MxMEventDefinition treeChopDefinition = null;
        //当前角色状态
        private CharacterState curState = CharacterState.General;
        public CharacterState CurrentState
        {
            get
            {
                return curState;
            }
            set
            {
                curState = value;
            }
        }

        [Header("Input Profiles")]
        [SerializeField]
        private MxMInputProfile m_generalLocomotion = null;

        [SerializeField]
        private MxMInputProfile m_strafeLocomotion = null;

        [SerializeField]
        private MxMInputProfile m_sprintLocomotion = null;

        //[SerializeField]
        //private LayerMask layerMask = new LayerMask();

        //用于处理角色旋转
        private Quaternion targetRotation;
        private Vector3 lastPosition;
        private Vector3 curVelocity;
        //Properties for test
        private Vector3 contact;
        private Vector3 moveDelta;
        private float rotSpeed;
        //Jump动作发生的延迟时间
        private float jumpTimer;
        private float jumpWindup;
        public enum CharacterState
        {
            General,
            Jumping,
            Logging,
            Building
        }
        void Start()
        {
            mxmAnimator = GetComponent<MxMAnimator>();
            rootMotionAplicator = GetComponent<MxMRootMotionApplicator>();
            trajectoryGenerator = GetComponent<MxMTrajectoryGenerator>();
            locomotionSpeedRamp = GetComponent<LocomotionSpeedRamp>();
            trajectoryGenerator.InputProfile = m_generalLocomotion;
            rotSpeed = 3f;
            targetRotation = transform.rotation;

            //进入strafe模式
            /*mxmAnimator.AddRequiredTag("Strafe");
            mxmAnimator.SetCalibrationData("Strafe");
            mxmAnimator.SetFavourCurrentPose(true, 0.95f);
            //m_locomotionSpeedRamp.ResetFromSprint();
            mxmAnimator.AngularErrorWarpRate = 360f;
            mxmAnimator.AngularErrorWarpThreshold = 270f;
            mxmAnimator.AngularErrorWarpMethod = EAngularErrorWarpMethod.TrajectoryFacing;
            trajectoryGenerator.TrajectoryMode = ETrajectoryMoveMode.Strafe;
            //trajectoryGenerator.InputProfile = m_strafeLocomotion;
            mxmAnimator.PastTrajectoryMode = EPastTrajectoryMode.CopyFromCurrentPose;*/
        }
        void Update()
        {
            
            switch (curState)
            {
                case CharacterState.General:
                    {
                        UpdateGeneral();
                    }
                    break;
                case CharacterState.Jumping:
                    {
                        UpdateJump();
                    }
                    break;
                case CharacterState.Logging:
                    {
                        UpdateLogging();
                    }
                    break;
                case CharacterState.Building:
                    {
                        UpdateBuilding();
                    }
                    break;
            }
            /*targetRotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * rotSpeed, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);*/
        }
        void UpdateLogging()
        {
            if (mxmAnimator.IsEventComplete)
            {
                curState = CharacterState.General;
            }
        }
        void UpdateBuilding()
        {

        }
        void UpdateGeneral()
        {
            /*if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                locomotionSpeedRamp.BeginSprint();
                trajectoryGenerator.MaxSpeed = 6.7f;
                trajectoryGenerator.PositionBias = 6f;
                trajectoryGenerator.DirectionBias = 6f;
                mxmAnimator.SetCalibrationData("Sprint");
                trajectoryGenerator.InputProfile = m_sprintLocomotion;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                locomotionSpeedRamp.ResetFromSprint();
                trajectoryGenerator.MaxSpeed = 4.3f;
                trajectoryGenerator.PositionBias = 10f;
                trajectoryGenerator.DirectionBias = 10f;
                mxmAnimator.SetCalibrationData("General");
                trajectoryGenerator.InputProfile = m_generalLocomotion;
            }*/
            // 跳跃
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (jumpDefinition != null)
                {
                    mxmAnimator.BeginEvent(jumpDefinition);
                    //修改rootmotion模式为代码控制
                    mxmAnimator.RootMotion = EMxMRootMotion.Off;
                    moveDelta.y = 3.5f;
                    rootMotionAplicator.EnableGravity = true;
                    curState = CharacterState.Jumping;
                }
            }
            // 伐木
            if (Input.GetMouseButtonDown(0))
            {
                if (treeChopDefinition != null)
                {
                    mxmAnimator.BeginEvent(treeChopDefinition);
                }
            }
            if (rootMotionAplicator.EnableGravity)
            {
                moveDelta.y -= 9.8f * Time.deltaTime;
            }
        }
        // 挥动伐木动画到进行判断的帧
        void SwingAx()
        {
            curState = CharacterState.Logging;
        }
        // Show Event contact
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(contact, Vector3.one);
            //Gizmos.DrawSphere();
        }
        void UpdateJump()
        {
            //在Jump过程中可以使用输入改变角色位置
            float vertical = Input.GetAxis("Vertical") * 5f;
            float horizontal = Input.GetAxis("Horizontal") * 5f;
            moveDelta = new Vector3(horizontal, moveDelta.y, vertical);
            moveDelta = transform.TransformDirection(moveDelta);
            //应用重力
            if (rootMotionAplicator.EnableGravity)
            {
                moveDelta.y -= 9.8f * Time.deltaTime;
            }
            rootMotionAplicator.ControllerWrapper.Move(moveDelta * Time.deltaTime);
            //Jump过程结束
            if (mxmAnimator.IsEventComplete)
            {
                curState = CharacterState.General;
                mxmAnimator.RootMotion = EMxMRootMotion.RootMotionApplicator;
                rootMotionAplicator.EnableGravity = true;
                //m_lastPosition = transform.position;
                //m_curVelocity = Vector3.zero;
            }
        }

    }
}

