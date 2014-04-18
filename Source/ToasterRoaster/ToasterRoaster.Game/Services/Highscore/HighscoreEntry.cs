using System.Runtime.Serialization;

namespace ToasterRoaster.Game.Services.Highscore
{
	[DataContract]
	public class HighscoreEntry
	{
		[DataMember]
		public string Id { get; set; }

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public int Points { get; set; }
		
		[DataMember]
		public int Level { get; set; }
	}
}