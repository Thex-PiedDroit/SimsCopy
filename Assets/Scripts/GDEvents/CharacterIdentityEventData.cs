

public class CharacterIdentityEventData : EventData
{
	public Character Subject { get; } = null;


	public CharacterIdentityEventData(Character character)
	{
		Subject = character;
	}
}
