
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;


[CreateAssetMenu(fileName = "NeedStateDisplayDataSet", menuName = "DataSets/UI/NeedStateDisplayDataSet")]
public class NeedStateDisplayDataSet : ScriptableObject
{
	[System.Serializable]
	public class DataSetDictionary : SerializableDictionaryBase<ENeedState, Color> { }

	[SerializeField]
	private DataSetDictionary m_dataSet = null;


	public Color GetColorForState(ENeedState needState)
	{
		return m_dataSet[needState];
	}
}
