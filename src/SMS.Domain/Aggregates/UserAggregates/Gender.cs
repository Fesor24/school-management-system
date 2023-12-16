using System.Runtime.Serialization;

namespace SMS.Domain.Aggregates.UserAggregates;
public enum Gender
{
    [EnumMember(Value = "Unspecified")]
    Unspecified,
    [EnumMember(Value = "Male")]
    Male,
    [EnumMember(Value = "Female")]
    Female,
}
