using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SurvivalGames.Util;

public class WieldableHandler : CharacterBehaviour, IWieldableHandler
{
    public Equipment curEquipment;
    [SerializeField]
    Transform gunTransform;
    [SerializeField]
    Transform rightHand;
    private List<GameObject> weaponList = new List<GameObject>();
    private ICharacterAnimationController m_CharacterAnimationController;
    private ICharacterUIHandler m_CharacterUIHandler;
    [SerializeField]
    private InputContextGroup m_RifleGroup;
    public GameObject Hammer { get => hammer; }
    [SerializeField]
    private string hammerPath;
    private GameObject hammer;
    protected override void OnBehaviourEnabled()
    {
        GetModule(out m_CharacterAnimationController);
        GetModule(out m_CharacterUIHandler);
        curEquipment = null;
        //����hammer
        GameObject hammerObj = Resources.Load<GameObject>(hammerPath);
        hammer = Instantiate(hammerObj, rightHand);
        hammer.transform.localPosition = Vector3.zero;
        hammer.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        hammer.transform.localScale = Vector3.one;
        hammer.SetActive(false);
        
    }
    private void OnFirstChange()
    {
        //Ԥ�������е�����
        for (int i = 0; i < GridManager.Instance.weaponItemDatas.Count; i++)
        {
            GameObject weaponObj = Resources.Load<GameObject>(GridManager.Instance.weaponItemDatas[i].prefabPath);
            GameObject weapon = Instantiate(weaponObj, gunTransform);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            weapon.transform.localScale = Vector3.one;
            weapon.SetActive(false);
            weaponList.Add(weapon);
        }
    }
    //�������ʹ��
    public void OnMouseButtonDown()
    {
        if (curEquipment.GetComponent<Weapon>())
        {
            Weapon curWeapon = curEquipment.GetComponent<Weapon>();
            m_CharacterUIHandler.EnableAimShake();
            curWeapon.OnFire();
        }
    }
    public void ChangeEquipmentByIndex(int index)
    {
        if (weaponList.Count == 0)
        {
            OnFirstChange();
        }
        //����Ƿ�����,����Ĭ�ϱ�Ϊnull�˳���������״̬
        if (index >= weaponList.Count || index < 0)
        {
            if (curEquipment != null)
            {
                curEquipment.gameObject.SetActive(false);
                curEquipment = null;
            }
            InputManager.PopContext(m_RifleGroup);
            m_CharacterUIHandler.CloseAimUI();
            m_CharacterAnimationController.ExitRifling();
            
            DebugUtil.Log("no Equipment is found!");
            return;
        }
        //���û�г���ǹ��������ǹ״̬�������л�ǹ
        if (curEquipment == null)
        {
            curEquipment = weaponList[index].GetComponent<Equipment>();
            curEquipment.gameObject.SetActive(true);
            //�������ر���
            InputManager.PushContext(m_RifleGroup);
            m_CharacterUIHandler.ShowAimUI();
            m_CharacterAnimationController.BeginRifling();
        }
        else
        {
            //�������ǹ������index���ǵ�ǰ������������ǹ
            if (curEquipment.Equals(weaponList[index].GetComponent<Equipment>()))
            {
                return;
            }
            curEquipment.gameObject.SetActive(false);
            curEquipment = weaponList[index].GetComponent<Equipment>();
            curEquipment.gameObject.SetActive(true);
            m_CharacterAnimationController.BeginEventing(EventDefinitionType.ItemChange);
        }
    }
    void ChangeEquipment(EquipmentObject equipment)
    {
        if (equipment == null)
        {
            //�л��س���״̬
        }
        else
        {
            //װ������
            //����װ������
            //����װ������״̬
        }
    }
}
