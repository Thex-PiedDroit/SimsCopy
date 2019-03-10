

public class CharacterIdentityEventData : EventData
{
	public Character Subject { get; } = null;


	public CharacterIdentityEventData(Character pCharacter)
	{
		Subject = pCharacter;
	}
}
