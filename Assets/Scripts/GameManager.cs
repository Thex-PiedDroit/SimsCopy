
using UnityEngine;


public class GameManager : MonoBehaviour
{
#region Variables (serialized)

	[SerializeField]
	private EventObject OnControlledCharacterChanged = null;

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

	private void OnDestroy()
	{
		ClearEventObjects();
	}

	private void SeizeControlOfCharacter(Character pCharacter)
	{
		m_pCurrentlyCharacter = pCharacter;

		OnControlledCharacterChanged?.FireEvent(new CharacterIdentityEventData(pCharacter));
	}

	private void ClearEventObjects()
	{
		if (OnControlledCharacterChanged != null)
			OnControlledCharacterChanged.OnEventFired = null;
	}

	private void FixedUpdate()
	{
		Time.timeScale = m_fTimeScale;
	}
}
