using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupMemberDefinition<T,Group>: DataDefinition<T> where T : GroupMemberDefinition<T, Group> where Group : GroupDefinition<Group, T>
{
    public Group ParentGroup => m_ParentGroup;
    public bool IsPartOfGroup => m_ParentGroup != null;
    public override string FullName
    {
        get
        {
            string categoryName = ParentGroup != null ? ParentGroup.Name : k_UnssignedGroup;
            return $"( {categoryName} ) / {Name}";
        }
    }

    [SerializeField]
    private Group m_ParentGroup;

    private const string k_UnssignedGroup = "No Group";
}
