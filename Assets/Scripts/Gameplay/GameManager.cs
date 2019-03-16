
using UnityEngine;


public class GameManager : MonoBehaviour
{
#region Variables (serialized)

	[SerializeField]
	private Character m_pTEMP_DefaultCharacter = null;

	#endregion

#region Variables (public)

	public float m_fTimeScale = 1.0f;

	#endregion

#region Variables (private)

	private Character m_pCurrentlyCharacter = null;

	#endregion


	private void Start()
	{
		SeizeControlOfCharacter(m_pTEMP_DefaultCharacter);
	}

	private void SeizeControlOfCharacter(Character pCharacter)
	{
		m_pCurrentlyCharacter = pCharacter;

		EventsHandler.Dispatch(EGenericGameEvents.CONTROLLED_CHARACTER_CHANGED, pCharacter);
	}

	private void FixedUpdate()
	{
		Time.timeScale = m_fTimeScale;
	}
}
