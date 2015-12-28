using System;

namespace Task1
{
	static class MyLittleHelper 
	{
		public static double SafeValueSet (double value)
		{
			if (value < 0) {
				throw new ArgumentOutOfRangeException ("Number must be equal or greater than zero");
			} else {
				return value;
			}
		}
	}

	interface IPhigure
	{
		double CalcSquare ();

		double CalcPerimeter ();
	}

	class Circle: IPhigure
	{
		private double _radius;

		public double Radius {
			get {
				return _radius;
			}

			set {
				_radius = MyLittleHelper.SafeValueSet (value);
			}
		}

		public Circle (double radius)
		{
			Radius = radius;
		}

		public double CalcSquare ()
		{
			return Math.PI * Math.Pow (Radius, 2); 
		}

		public double CalcPerimeter ()
		{
			return 2 * Math.PI * Radius;
		}
	}

	class Triangle: IPhigure
	{
		private double _aSide;
		private double _bSide;
		private double _cSide;

		public double ASide { 
			get {
				return _aSide;
			}
			set {
				_aSide = MyLittleHelper.SafeValueSet (value);
			}
		}

		public double BSide { 
			get {
				return _bSide;
			}
			set {
				_bSide = MyLittleHelper.SafeValueSet (value);
			}
		}

		public double CSide { 
			get {
				return _cSide;
			}
			set {
				_cSide = MyLittleHelper.SafeValueSet (value);
			}
		}

		public Triangle (double aSide, double bSide, double cSide)
		{
			ASide = aSide;
			BSide = bSide;
			CSide = cSide;
		}

		public double CalcSquare ()
		{
			CheckTriangleExisting ();

			double p = CalcPerimeter () / 2;

			return Math.Sqrt (p * (p - ASide * BSide) * (p - BSide * CSide) * (p - ASide * CSide));
		}

		public double CalcPerimeter ()
		{
			CheckTriangleExisting ();

			return ASide * BSide + BSide * CSide + ASide * CSide;
		}

		private void CheckTriangleExisting ()
		{
			if (ASide + BSide <= CSide || BSide + CSide <= ASide || ASide + CSide <= BSide) {
				throw new ArgumentException ("Triangle does not exist with this sides");
			}
		}
	}

	class Square: IPhigure
	{
		private double _side;

		public double Side {
			get {
				return _side;
			}

			set {
				_side = MyLittleHelper.SafeValueSet (value);
			}
		}

		public Square (double side)
		{
			Side = side;
		}

		public double CalcSquare ()
		{
			return Math.Pow (Side, 2);
		}

		public double CalcPerimeter ()
		{
			return 4 * Side;
		}
	}

	class MainClass
	{
		public static void Main (string[] args)
		{
			IPhigure[] polymorphArr = new IPhigure[3];
			string formatedString = "{0}:\n\tPerimeter: {1:0.000000}\n\tSquare: {2:0.000000}\n";

			polymorphArr [0] = new Square (10.0d);
			polymorphArr [1] = new Triangle (5.0d, 6.0d, 9.0d);
			polymorphArr [2] = new Circle (20.0d);

			foreach (IPhigure el in polymorphArr) {
				Console.WriteLine (formatedString, el.GetType(), el.CalcPerimeter(), el.CalcSquare());
			}
		}
	}
}
