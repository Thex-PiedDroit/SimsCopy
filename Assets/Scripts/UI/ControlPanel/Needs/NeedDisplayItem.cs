
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class NeedDisplayItem : MonoBehaviour
{
#region Variables (serialized)

	[SerializeField]
	private NeedTypesDisplayDataSet m_pDisplayDataSet = null;

	[SerializeField]
	private Image m_pIcon = null;
	[SerializeField]
	private TextMeshProUGUI m_pNameLabel = null;
	[SerializeField]
	private Image m_pGaugeFill = null;

	#endregion

#region Variables (private)

	const float MIN_GAUGE_SCALE = 0.05f;


	private ENeedType m_eAssociatedNeed = ENeedType.NONE;

	private NeedTypesDisplayData m_pNeedTypeDisplayData;
	private NeedStateDisplayDataSet m_pNeedStateDisplayData;

	#endregion


	public void InitializeWithNeedType(ENeedType eNeedType)
	{
		m_eAssociatedNeed = eNeedType;
		FetchDataForNeedType(eNeedType);

		InitIcon();
		InitName();
	}

	private void FetchDataForNeedType(ENeedType eNeedType)
	{
		m_pNeedTypeDisplayData = m_pDisplayDataSet.GetDataForNeed(eNeedType);
		m_pNeedStateDisplayData = m_pNeedTypeDisplayData.m_pStateDisplayData;
	}

	private void InitIcon()
	{
		m_pIcon.sprite = m_pNeedTypeDisplayData.m_pSprite;
	}

	private void InitName()
	{
		m_pNameLabel.text = m_pNeedTypeDisplayData.m_sDisplayName;
	}

	public void UpdateGauge(Need pNeed)
	{
		UpdateGaugeFilling(pNeed.Satisfaction);
		UpdateGaugeColor(pNeed.State);
	}

	private void UpdateGaugeFilling(float fSatisfaction)
	{
		float fFillingPercent = fSatisfaction / NeedsToolkit.F_NEEDS_MAX_SATISFACTION;
		fFillingPercent = Mathf.Max(MIN_GAUGE_SCALE, fFillingPercent);

		SetGaugeScale(fFillingPercent);
	}

	private void SetGaugeScale(float fScale)
	{
		Vector3 tScale = m_pGaugeFill.transform.localScale;
		tScale.x = fScale;
		m_pGaugeFill.transform.localScale = tScale;
	}

	private void UpdateGaugeColor(ENeedState eNeedState)
	{
		m_pGaugeFill.color = m_pNeedStateDisplayData.GetColorForState(eNeedState);
	}

	public ENeedType GetAssociatedNeed()
	{
		return m_eAssociatedNeed;
	}
}
