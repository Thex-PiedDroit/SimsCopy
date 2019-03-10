
using System.Collections.Generic;
using UnityEngine;


public class DataSetsManager : ScriptableObject
{
#region Variables (serialized)

	[SerializeField]
	private List<DictionaryDataSet> m_pAllDataSets = null;

	#endregion


	public void InitializeAllDataSets()
	{
		for (int i = 0; i < m_pAllDataSets.Count; ++i)
		{
			m_pAllDataSets[i].InitializeDictionary();
		}
	}

	public void UninitializeAllDataSets()
	{
		for (int i = 0; i < m_pAllDataSets.Count; ++i)
		{
			m_pAllDataSets[i].UninitializeDictionary();
		}
	}
}
