using System;
using System.Collections.Generic;

namespace Noise.Utility {
	
	public struct Colour {

		public double R { get; set; }
		public double G { get; set; }
		public double B { get; set; }
		public double A { get; set; }

		public Colour(int r, int g, int b, int a) : this(r / 255.0, g / 255.0, b / 255.0, a / 255.0) {
		}
		public Colour(double r, double g, double b, double a) : this() {
			R = r;
			G = g;
			B = b;
			A = a;
		}

		public static Colour Lerp(Colour a, Colour b, double t) {
			double cr = Math.Lerp(a.R, b.R, t);
			double cg = Math.Lerp(a.G, b.G, t);
			double cb = Math.Lerp(a.B, b.B, t);
			double ca = Math.Lerp(a.A, b.A, t);

			return new Colour(cr, cg, cb, ca);
		}
		
	}

	public class Gradient {
		
		struct Point : IComparable<Point> {

			public double Pos { private set; get; }
			public Colour Colour { private set; get; }

			public Point(double pos, Colour colour) : this() {
				Pos = pos;
				Colour = colour;
			}

			public int CompareTo(Point other) {
				return Pos.CompareTo(other.Pos);
			}

		}

		private List<Point> colours = new List<Point>();

		public Colour this[double t] {
			get {
				int index = 0;
				for (index = 0; index < colours.Count; ++index) {
					if (t < colours[index].Pos) {
						break;
					}
				}

				int index0 = Math.Clamp(index - 1, 0, colours.Count);
				int index1 = Math.Clamp(index, 0, colours.Count);

				if (index0 == index1) {
					return colours[index0].Colour;
				}

				double pos0 = colours[index0].Pos;
				double pos1 = colours[index1].Pos;
				double alpha = (t - pos0) / (pos1 - pos0);

				return Colour.Lerp(colours[index0].Colour, colours[index1].Colour, (float)alpha);
			}
		}

		public int Count {
			get { return colours.Count; }
		}

		public void Add(double p, Colour colour) {
			colours.Add(new Point(p, colour));
			colours.Sort();
		}

		public void Clear() {
			colours.Clear();
		}

	}

}
