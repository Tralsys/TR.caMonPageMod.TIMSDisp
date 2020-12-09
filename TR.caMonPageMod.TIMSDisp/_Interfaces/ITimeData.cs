namespace TR.caMonPageMod.TIMSDisp._Interfaces
{
	public interface ITimeData
	{
		public bool IsVisible { get; set; }
		public int HH { get; set; }
		public int MM { get; set; }
		public int SS { get; set; }
		public bool IsHHVisible { get; set; }
		public bool IsMMVisible { get; set; }
		public bool IsSSVisible { get; set; }

		public string Separator { get; set; }
		public bool IsSeparatorVisible { get; set; }
	}

	public class PureTimeData : ITimeData
	{
		public bool IsVisible { get; set; }
		public int HH { get; set; }
		public int MM { get; set; }
		public int SS { get; set; }
		public bool IsHHVisible { get; set; }
		public bool IsMMVisible { get; set; }
		public bool IsSSVisible { get; set; }
		public string Separator { get; set; }
		public bool IsSeparatorVisible { get; set; }
	}
}
