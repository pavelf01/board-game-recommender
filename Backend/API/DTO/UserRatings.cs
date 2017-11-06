using System.Runtime.Serialization;

namespace API.DTO
{
    [DataContract]
    public class UserRatings
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public float Value { get; set; }
        [DataMember]
        public string Comment { get; set; }
        [DataMember]
        public int BoardGameId { get; set; }
    }
}