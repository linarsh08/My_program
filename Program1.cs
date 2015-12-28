using System;
using System.Collections.Generic;

namespace Task2
{
	class RootFoundHelper
	{
		public static double[] ByIntersectingLines (double firstX, double secondX, double eps)
		{
			Swap (ref firstX, ref secondX);
			eps = Math.Abs (eps);

			List<double> stackOfVars = new List<double> (new double[2] { firstX, secondX });
			List<double> stackOfFuncRes = new List<double> (ResultsOfFunction (firstX, secondX));
			double[] results;
			int stepCount = 0;

			while (Math.Abs(stackOfVars[1] - stackOfVars[0]) > eps) {
				stackOfVars.Add (ZeroYFinder (stackOfVars, stackOfFuncRes));
				stackOfVars.RemoveAt (0);

				stackOfFuncRes.Add (Func (stackOfVars [1]));
				stackOfFuncRes.RemoveAt (0);

				if (stepCount++ > int.MaxValue) {
					throw new OverflowException ("To many steps");
				}
			}

			results = stackOfVars.ToArray ();
			SwapInMinus (ref results[0], ref results[1]);
			return results;
		}

		public static double[] ByHorda (double firstX, double secondX, double eps)
		{
			Swap (ref firstX, ref secondX);
			eps = Math.Abs (eps);

			int stepCount = 0;

			while (Math.Abs(firstX - secondX) > eps) {
				firstX = ZeroYFinder (firstX, secondX);
				secondX = ZeroYFinder (secondX, firstX);

				if (stepCount++ > int.MaxValue) {
					throw new OverflowException ("To many steps");
				}
			}

			SwapInMinus (ref firstX, ref secondX);
			return new double[] { firstX, secondX };
		}


		public static double[] ByBinary (double firstX, double secondX, double eps)
		{
			// CheckDifferentSigns (firstX, secondX);
			SwapInMinus (ref firstX, ref secondX);

			eps = Math.Abs (eps);

			int stepCount = 0;
			double tempX = firstX;

			while (Math.Abs(Func(tempX)) > eps) {
				tempX = (secondX + firstX) / 2.0d;

				if (Func(tempX) * Func(firstX) < 0.0d) {
					secondX = tempX;
				} else {
					firstX = tempX;
				}

				if (stepCount++ > int.MaxValue) {
					throw new OverflowException ("To many steps");
				}

			}

			SwapInMinus (ref firstX, ref secondX);

			return new double[] { firstX, secondX };
		}


		private static double Func (double inputX)
		{
			return (double)(Math.Cos (inputX) - Math.Pow (inputX, 3));
		}

		private static void Swap (ref double first, ref double second)
		{
			double temp;

			if (Math.Abs (Func (first)) < Math.Abs (Func (second))) {
				temp = first;
				first = second;
				second = temp;
			}
		}

		private static void SwapInMinus(ref double first, ref double second)
		{
			double temp;

			if (first > second) {
				temp = first;
				first = second;
				second = temp;
			}
		}

		private static double[] ResultsOfFunction (double firstX, double secondX)
		{
			return new double[2] { Func(firstX), Func(secondX) };
		}

		private static double ZeroYFinder (List<double> stackOfVars, List<double> stackOfFuncRes)
		{
			return stackOfVars [0] - ((stackOfFuncRes [0] * (stackOfVars [1] - stackOfVars [0])) / (stackOfFuncRes [1] - stackOfFuncRes [0]));
		}

		private static double ZeroYFinder (double firstX, double secondX)
		{
			return secondX - ((Func (secondX) * (secondX - firstX)) / (Func (secondX) - Func (firstX)));
		}

		private static void CheckDifferentSigns (double firstX, double secondX)
		{
			if (firstX * secondX > 0) {
				throw new Exception ("Cant find answer");
			}
		}
	}

	class MainClass
	{
		public delegate double[] Del (double firstX, double SecondX, double eps); 
		public static void Main (string[] args)
		{
			Del handler = new Del (RootFoundHelper.ByBinary);

			double[] ararrr;
			ararrr = handler (-10.0d, 10d, 0.00000001d);

			Console.WriteLine ("BinaryTree: [{0}, {1}]", ararrr[0], ararrr[1]);

			handler = RootFoundHelper.ByHorda;
			ararrr = handler (-10.0d, 10.0d, 0.0000001d);

			Console.WriteLine ("HordaTree: [{0}, {1}] ", ararrr[0], ararrr[1]);

			handler = RootFoundHelper.ByIntersectingLines;
			ararrr = handler (5d, 10d, 0.00000000001d);

			Console.WriteLine ("IntersectingLinesTree: [{0}, {1}]", ararrr [0], ararrr [1]);
		}
	}
}
