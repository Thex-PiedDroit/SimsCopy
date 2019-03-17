
using UnityEngine;


public class GameManager : MonoBehaviour
{
#region Variables (serialized)

	[SerializeField]
	private Character m_TEMP_defaultCharacter = null;

	#endregion

#region Variables (public)

	[Range(0.01f, 100.0f)]
	public float m_timeScale = 1.0f;

	#endregion

#region Variables (private)

	private Character m_currentCharacter = null;

	#endregion


	private void Start()
	{
		SeizeControlOfCharacter(m_TEMP_defaultCharacter);
	}

	private void SeizeControlOfCharacter(Character character)
	{
		m_currentCharacter = character;

		EventsHandler.Dispatch(EGenericGameEvents.CONTROLLED_CHARACTER_CHANGED, character);
	}

	private void FixedUpdate()
	{
		Time.timeScale = m_timeScale;
	}
}
