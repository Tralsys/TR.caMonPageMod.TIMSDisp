namespace TR.caMonPageMod.TIMSDisp._Interfaces
{
	public interface IStartEndDistance
	{
		/// <summary>開始位置の距離程情報</summary>
		public double StartDistance { get; set; }
		/// <summary>終了位置の距離程情報</summary>
		public double EndDistance { get; set; }
	}

	public class PureStartEndDistance : IStartEndDistance
	{
		public double StartDistance { get; set; }
		public double EndDistance { get; set; }
	}
}
