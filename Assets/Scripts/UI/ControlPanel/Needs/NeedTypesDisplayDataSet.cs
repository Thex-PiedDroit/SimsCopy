
using RotaryHeart.Lib.SerializableDictionary;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "NeedTypesDisplayDataSet", menuName = "DataSets/UI/NeedTypesDisplayDataSet")]
public class NeedTypesDisplayDataSet : ScriptableObject
{
	[Serializable]
	public class DataSetDictionary : SerializableDictionaryBase<ENeedType, NeedTypesDisplayData> { }

	[SerializeField]
	private DataSetDictionary m_pDataSet = null;


	public NeedTypesDisplayData GetDataForNeed(ENeedType eNeedType)
	{
		return m_pDataSet[eNeedType];
	}
}

[Serializable]
public struct NeedTypesDisplayData
{
	public string m_sDisplayName;
	public Sprite m_pSprite;
	public NeedStateDisplayDataSet m_pStateDisplayData;
}
