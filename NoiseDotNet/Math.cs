
namespace Noise {

    static class Math {

        public const double DEGS_TO_RADS = System.Math.PI / 180.0;

		public static int Clamp(int n, int min, int max) {
			return ((n < min) ? min : ((n > max) ? max : n));
		}
		public static double Clamp(double n, double min, double max) {
			return ((n < min) ? min : ((n > max) ? max : n));
		}

        public static double Lerp(double a, double b, double t) {
            return ((1.0 - t) * a) + (t * b);
        }

        public static double CubicInterp(double a, double b, double c, double d, double t) {
            double p = (d - c) - (a - b);
            double q = (a - b) - p;
            double r = c - a;
            double s = b;

            return (p * t * t * t) + (q * t * t) + (r * t) + s;
        }

        public static double CubicSCurve(double t) {
            return t * t * (3.0 - 2.0 * t);
        }

        public static double QuinticSCurve(double t) {
            double t3 = t * t * t;
            double t4 = t3 * t;
            return (6.0 * (t4 * t)) - (15.0 * t4) + (10.0 * t3);
        }

    }

}
