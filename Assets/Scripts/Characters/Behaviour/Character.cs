
using UnityEngine;

public class Character : MonoBehaviour
{
#region Variables (private)

	private NeedsUpdater m_pNeedsUpdater = null;

	#endregion


	private void Start()
	{
		m_pNeedsUpdater = new NeedsUpdater();
	}

	private void Update()
	{
		m_pNeedsUpdater.UpdateNeeds();
		CheckForNeedsReaction();
	}

	private void CheckForNeedsReaction()
	{
		ENeedType eNeedType = m_pNeedsUpdater.GetHighestPriorityNeed();

		Debug.Log(string.Format("Highest priority need = {0}", eNeedType));
	}

	public NeedsUpdater GetNeedsUpdater()
	{
		return m_pNeedsUpdater;
	}
}
