using Newtonsoft.Json;

namespace Ste.Framework.Common;

[Serializable]
public class TokenUserInformation
{
    [JsonProperty("a")]
    public int UserId { get; set; }

    [JsonProperty("b")]
    public string? Name { get; set; }

    [JsonProperty("c")]
    public string? Family { get; set; }

    [JsonProperty("e")]
    public long? NationalCode { get; set; }

    [JsonProperty("g")]
    public long PhoneNumber { get; set; }

    [JsonProperty("h")]
    public string? Username { get; set; }

    [JsonProperty("j")]
    public byte Status { get; set; }

    [JsonProperty("m")]
    public byte? UnsuccessfulLoginCount { get; set; }

    [JsonProperty("n")]
    public long? LocationId { get; set; }

    [JsonProperty("p")]
    public string? Province { get; set; }

    [JsonProperty("q")]
    public string? County { get; set; }

    [JsonProperty("r")]
    public int? ProvinceId { get; set; }

    [JsonProperty("s")]
    public int? CountyId { get; set; }

    [JsonProperty("v")]
    public bool? CenterStatus { get; set; }

    [JsonProperty("w")]
    public byte? ResumeStatus { get; set; }

    [JsonProperty("x")]
    public int? GenderId { get; set; }

    [JsonProperty("y")]
    public bool? RegisterFromGovernment { get; set; }
}