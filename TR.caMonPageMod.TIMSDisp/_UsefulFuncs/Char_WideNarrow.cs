using System;
using System.Diagnostics;
using System.Windows;

using Microsoft.VisualBasic;

namespace TR.caMonPageMod.TIMSDisp._UsefulFuncs
{
	public static partial class UsefulFuncs
	{
		public static object WideNarrowConv(DependencyObject d, object baseValue)
		{

			if (baseValue is string s)
				try {
					bool IsHankaku = (d as IChar_WideNarrow)?.IsHankaku ?? false;//通常は全角に変換
					return Strings.StrConv(s, IsHankaku ? VbStrConv.Narrow : VbStrConv.Wide, 0x411);
				}catch(Exception e)
				{
					Debug.WriteLine("Str:{0}\n{1}", s, e);
					throw;
				}
			else
				throw new ArgumentException("Entered baseValue is not string");
		}

	}

	public interface IChar_WideNarrow
	{
		public bool IsHankaku { get; }
	}
	public class Char_WideNarrowSetting : DependencyObject, IChar_WideNarrow
	{
		public bool IsHankaku { get; protected set; }

		public Char_WideNarrowSetting(bool isHankaku = false) => IsHankaku = isHankaku;
		

	}
}
