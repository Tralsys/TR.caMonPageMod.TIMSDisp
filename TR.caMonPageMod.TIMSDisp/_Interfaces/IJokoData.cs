namespace TR.caMonPageMod.TIMSDisp._Interfaces
{
	public interface IJokoData : IStartEndDistance
	{
		public int LimitSpeed { get; set; }
	}

	public class PureJokoData : IJokoData
	{
		public int LimitSpeed { get; set; }
		public double StartDistance { get; set; }
		public double EndDistance { get; set; }
	}
}
