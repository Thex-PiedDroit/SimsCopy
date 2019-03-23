
using RotaryHeart.Lib.SerializableDictionary;
using System;
using UnityEngine;


[CreateAssetMenu(fileName = "NeedTypesDisplayDataSet", menuName = "DataSets/UI/NeedTypesDisplayDataSet")]
public class NeedTypesDisplayDataSet : ScriptableObject
{
	[Serializable]
	public class DataSetDictionary : SerializableDictionaryBase<ENeedType, NeedTypesDisplayData> { }

	[SerializeField]
	private DataSetDictionary m_dataSet = null;


	public NeedTypesDisplayData GetDataForNeed(ENeedType needType)
	{
		return m_dataSet[needType];
	}
}

[Serializable]
public struct NeedTypesDisplayData
{
	public string m_displayName;
	public Sprite m_sprite;
	public NeedStateDisplayDataSet m_stateDisplayData;
}
