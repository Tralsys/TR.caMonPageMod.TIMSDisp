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
			if (d is IChar_WideNarrow wn && baseValue is string s)
				try {
					return Strings.StrConv(s, wn.IsHankaku ? VbStrConv.Narrow : VbStrConv.Wide, 0x411);
				}catch(Exception e)
				{
					Debug.WriteLine("Str:{0}\n{1}", s, e);
					throw;
				}
			else
				throw new ArgumentException("Entered DependencyObject is not IChar_WideNarrow, or baseValue is not string");
		}

	}

	public interface IChar_WideNarrow
	{
		public bool IsHankaku { get; }
	}
}
