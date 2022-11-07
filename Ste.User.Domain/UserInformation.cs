namespace Ste.User.Domain;

public class UserInformation
{
    public int UserId { get; set; }
    public string? Name { get; set; }
    public string? Family { get; set; }
    public string? FatherName { get; set; }
    public long? NationalCode { get; set; }
    public long PhoneNumber { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public byte Status { get; set; }
    public DateTime CreateAt { get; set; }
    public byte? UnsuccessfulLoginCount { get; set; }
    public long? LocationId { get; set; }
    public string? Province { get; set; }
    public string? County { get; set; }
    public int? ProvinceId { get; set; }
    public int? CountyId { get; set; }
    public bool? CenterStatus { get; set; }
    public byte? ResumeStatus { get; set; }
    public int? GenderId { get; set; }
    public bool? RegisterFromGovernment { get; set; }
    public bool Verified { get; set; }
}
