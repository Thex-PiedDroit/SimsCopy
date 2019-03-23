
using UnityEngine;

public class Character : MonoBehaviour
{
#region Variables (private)

	private NeedsUpdater m_needsUpdater = null;

	#endregion


	private void Start()
	{
		m_needsUpdater = new NeedsUpdater();
	}

	private void Update()
	{
		m_needsUpdater.UpdateNeeds();
		CheckForNeedsReaction();
	}

	private void CheckForNeedsReaction()
	{
		ENeedType needType = m_needsUpdater.GetHighestPriorityNeed();

		DebugTools.Log("[Character] - Highest priority need = {0}", needType);
	}

	public NeedsUpdater GetNeedsUpdater()
	{
		return m_needsUpdater;
	}
}
