using UnityEngine;
using UnityEngine.Rendering;
using SurvivalGames.Util;
using UnityEngine.Rendering.Universal;

public class PostProcessHandler : CharacterBehaviour, IPostProcess
{
    [SerializeField]
    private Volume m_volume;
    private DepthOfField m_depthOfField;
    void Start()
    {
        m_volume = GameObject.Find("PostProcess").GetComponent<Volume>();
        if (m_volume == null)
        {
            DebugUtil.LogError("volume is null");
        }
        m_volume.profile.TryGet(out m_depthOfField);

        if (m_depthOfField == null)
        {
            DebugUtil.LogError("Depth of Field is not available in the assigned Volume Profile.");
        }
    }
    void ToggleDepthOfField()
    {
        // 切换景深效果的启用状态
        m_depthOfField.active = !m_depthOfField.active;

    }
    public void BeginGlobalBlur()
    {
        ToggleDepthOfField();
        m_depthOfField.mode = new DepthOfFieldModeParameter(DepthOfFieldMode.Bokeh,false);
        m_depthOfField.focusDistance.value = 0.1f;
    }
    public void CloseGlobalBlur()
    {
        ToggleDepthOfField();
    }
}
