namespace API.Entities;

public class AppUser
{
	// Entity Framework NEEDS these properties to be public so it can set and get them 
	// [Key] - we COULD use this but we'll use conventions instead
	public int Id { get; set; } // the property MUST be called Id so that EF can use it as our primary key in our database. if we called it "TheId" instead it wouldn't do it automatically. it's convention.
	public string UserName { get; set; }
	public byte[] PasswordHash { get; set; }
	public byte[] PasswordSalt { get; set; }
}
