
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class NeedDisplayItem : MonoBehaviour
{
#region Variables (serialized)

	[SerializeField]
	private NeedTypesDisplayDataSet m_displayDataSet = null;

	[SerializeField]
	private Image m_icon = null;
	[SerializeField]
	private TextMeshProUGUI m_nameLabel = null;
	[SerializeField]
	private Image m_gaugeFill = null;

	#endregion

#region Variables (private)

	const float MIN_GAUGE_SCALE = 0.05f;


	private ENeedType m_associatedNeed = ENeedType.NONE;

	private NeedTypesDisplayData m_needTypeDisplayData;
	private NeedStateDisplayDataSet m_needStateDisplayData;

	#endregion


	public void InitializeWithNeedType(ENeedType needType)
	{
		m_associatedNeed = needType;
		FetchDataForNeedType(needType);

		InitIcon();
		InitName();
	}

	private void FetchDataForNeedType(ENeedType needType)
	{
		m_needTypeDisplayData = m_displayDataSet.GetDataForNeed(needType);
		m_needStateDisplayData = m_needTypeDisplayData.m_stateDisplayData;
	}

	private void InitIcon()
	{
		m_icon.sprite = m_needTypeDisplayData.m_sprite;
	}

	private void InitName()
	{
		m_nameLabel.text = m_needTypeDisplayData.m_displayName;
	}

	public void UpdateGauge(NeedStateInfo needStateInfo)
	{
		UpdateGaugeFilling(needStateInfo.m_satisfaction);
		UpdateGaugeColor(needStateInfo.m_state);
	}

	private void UpdateGaugeFilling(float satisfaction)
	{
		float fillingPercent = satisfaction / NeedsToolkit.NEEDS_MAX_SATISFACTION;
		fillingPercent = Mathf.Max(MIN_GAUGE_SCALE, fillingPercent);

		SetGaugeScale(fillingPercent);
	}

	private void SetGaugeScale(float newScale)
	{
		Vector3 scale = m_gaugeFill.transform.localScale;
		scale.x = newScale;
		m_gaugeFill.transform.localScale = scale;
	}

	private void UpdateGaugeColor(ENeedState needState)
	{
		m_gaugeFill.color = m_needStateDisplayData.GetColorForState(needState);
	}

	public ENeedType GetAssociatedNeed()
	{
		return m_associatedNeed;
	}
}
