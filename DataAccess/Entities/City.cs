#nullable disable

using AppCore.Records.Bases;

namespace DataAccess.Entities
{
	public class City : RecordBase
	{

        public string Name { get; set; }

        public int CountryId { get; set; }

        public Country Country { get; set; }

        public List<UserDetail> UserDetails { get; set; }

    }
}
