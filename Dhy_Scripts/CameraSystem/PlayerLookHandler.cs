using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Cinemachine;
using UnityEngine.Events;

public class PlayerLookHandler : MonoBehaviour, ILookHandler
{
    #region Attributes
    public Vector2 ViewAngle => m_ViewAngle;
    public Vector2 LookInput { get; private set; }
    public Vector2 LookDelta { get; private set; }
    [Header("Transforms")]

    [SerializeField]
    [Tooltip("Transform to rotate Up & Down.")]
    private Transform m_XTransform;

    [SerializeField]
    [Tooltip("Transform to rotate Left & Right.")]
    private Transform m_YTransform;

    [Header("Mesh Hiding")]
    public Transform[] hideRenderers;
    [Header("Character Material")]
    [SerializeField]
    private Material m_characterMaterial;

    [Header("Camera")]
    public CinemachineVirtualCamera firstLook;
    public CinemachineVirtualCamera thirdLook;
    public CinemachineVirtualCamera shoulderLook;

    public event UnityAction PostViewUpdate;

    private bool firstPerson = false;
    public Vector2 m_ViewAngle;
    [SerializeField, Range(0.1f, 10f)]
    [Tooltip("Rotation Speed.")]
    private float m_Sensitivity = 1.5f;
    private float m_CurrentSensitivity;
    private Vector2 m_AdditiveLook;
    [SerializeField]
    [Tooltip("The up & down rotation will be inverted, if checked.")]
    private bool m_Invert;
    [SerializeField]
    [Tooltip("Vertical look limits (in angles).")]
    private Vector2 m_LookLimits = new Vector2(-60f, 90f);
    private LookHandlerInputDelegate m_InputDelegate;
    private Camera m_FOVCamera;
    private bool m_HasFOVCamera;
    #endregion 
    void Start()
    {
        firstPerson = false;
        m_HasFOVCamera = m_FOVCamera != null;
        
    }
    void Update()
    {
    }
    void HideMeshes(bool firstPerson)
    {
        //将头的material参数调整为1.5,隐藏头
        if (firstPerson)
        {
            m_characterMaterial.SetFloat("_HeadHeight", 1.4f);
        }
        else
        {
            m_characterMaterial.SetFloat("_HeadHeight", 2f);
        }
        //隐藏头盔头套等
        foreach(Transform tf in hideRenderers)
        {
            if (firstPerson)
            {
                tf.transform.gameObject.SetActive(false);
            }
            else
            {
                tf.transform.gameObject.SetActive(true);
            }
        }
        /*foreach (Transform tf in hideRenderers)
            foreach (Renderer rend in tf.GetComponentsInChildren<Renderer>())
            {
                rend.shadowCastingMode = firstPerson ? ShadowCastingMode.ShadowsOnly : ShadowCastingMode.On;
            }*/
                //rend.shadowCastingMode = firstPerson ? ShadowCastingMode.ShadowsOnly : ShadowCastingMode.On;
    }

    public void SetLookInput(LookHandlerInputDelegate input) => m_InputDelegate = input;

    /*protected override void OnBehaviourEnabled()
    {
        if (!m_XTransform)
        {
            Debug.LogErrorFormat(this, "Assign the X Transform in the inspector!", name);
            enabled = false;
        }
        else if (!m_YTransform)
        {
            Debug.LogErrorFormat(this, "Assign the Y Transform in the inspector!", name);
            enabled = false;
        }

        m_HasFOVCamera = m_FOVCamera != null;

        GetModule<ICharacterMotor>().Teleported += OnTeleport;
    }*/
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            firstPerson = !firstPerson;
            HideMeshes(firstPerson);
            if (firstPerson)
            {
                thirdLook.gameObject.SetActive(false);
                firstLook.gameObject.SetActive(true);
            }
            else
            {
                firstLook.gameObject.SetActive(false);
                thirdLook.gameObject.SetActive(true);
            }
        }
        LookInput = Vector2.zero;

        if (m_InputDelegate != null)
            LookInput = m_InputDelegate();

        m_CurrentSensitivity = GetTargetSensitivity(m_CurrentSensitivity, Time.deltaTime * 8f);
        MoveView(LookInput);

        PostViewUpdate?.Invoke();
    }
    public void ChangeViewToFirstLook()
    {
        if (shoulderLook.gameObject.activeInHierarchy)
        {
            shoulderLook.gameObject.SetActive(false);
        }
        if (thirdLook.gameObject.activeInHierarchy)
        {
            thirdLook.gameObject.SetActive(false);
        }
        firstLook.gameObject.SetActive(true);
        HideMeshes(true);
    }
    public void ChangeViewToShoulderLook()
    {
        if (firstLook.gameObject.activeInHierarchy)
        {
            firstLook.gameObject.SetActive(false);
        }
        if (thirdLook.gameObject.activeInHierarchy)
        {
            thirdLook.gameObject.SetActive(false);
        }
        shoulderLook.gameObject.SetActive(true);
        HideMeshes(false);
    }
    public void ChangeViewToThirdLook()
    {
        if (firstLook.gameObject.activeInHierarchy)
        {
            firstLook.gameObject.SetActive(false);
        }
        if (shoulderLook.gameObject.activeInHierarchy)
        {
            shoulderLook.gameObject.SetActive(false);
        }
        thirdLook.gameObject.SetActive(true);
        HideMeshes(false);
    }
    private float GetTargetSensitivity(float currentSens, float delta)
    {
        float targetSensitivity = m_Sensitivity;
        targetSensitivity *= m_HasFOVCamera ? m_FOVCamera.fieldOfView / 90f : 1f;

        return Mathf.Lerp(currentSens, targetSensitivity, delta);
    }
    private void MoveView(Vector2 lookInput)
    {
        var prevAngle = m_ViewAngle;

        m_ViewAngle.x += lookInput.x * m_CurrentSensitivity * (m_Invert ? 1f : -1f);
        m_ViewAngle.y += lookInput.y * m_CurrentSensitivity;

        m_ViewAngle.x = ClampAngle(m_ViewAngle.x, m_LookLimits.x, m_LookLimits.y);
        LookDelta = new Vector2(m_ViewAngle.x - prevAngle.x, m_ViewAngle.y - prevAngle.y);

        var viewAngle = new Vector2()
        {
            x = ClampAngle(m_ViewAngle.x + m_AdditiveLook.x, m_LookLimits.x, m_LookLimits.y),
            y = m_ViewAngle.y + m_AdditiveLook.y
        };
        m_YTransform.localRotation = Quaternion.Euler(0f, viewAngle.y, 0f);
        m_XTransform.localRotation = Quaternion.Euler(m_XTransform.localRotation.eulerAngles.x, m_XTransform.localRotation.eulerAngles.y, viewAngle.x+m_XTransform.localRotation.eulerAngles.z);
    }
    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle > 360f)
            angle -= 360f;
        else if (angle < -360f)
            angle += 360f;

        return Mathf.Clamp(angle, min, max);
    }
}
