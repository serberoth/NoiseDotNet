using Noise.Model;

namespace Noise.Utility {

	public abstract class NoiseMapBuilder {

		public NoiseMap Destination { protected get; set; }
		public IModule Source { protected get; set; }
		
		public int Width { get; set; }
		public int Height { get; set; }

		public abstract void Build();

	}

	public class PlanarNoiseMapBuilder : NoiseMapBuilder {

		public double XLowerBound { get; set; }
		public double XUpperBound { get; set; }
		public double ZLowerBound { get; set; }
		public double ZUpperBound { get; set; }

		public bool Seemless { get; set; }

		public PlanarNoiseMapBuilder(double xlower, double xupper, double zlower, double zupper, bool seemless = false) {
			XLowerBound = xlower;
			XUpperBound = xupper;
			ZLowerBound = zlower;
			ZUpperBound = zupper;
			Seemless = seemless;
		}

		public override void Build() {
			Destination.Resize(Width, Height);

			var model = new PlaneModel(Source);

			double xextent = XUpperBound - XLowerBound;
			double zextent = ZUpperBound - ZLowerBound;
			double xdelta = xextent / (double)Width;
			double zdelta = zextent / (double)Height;
			double xcurr = XLowerBound;
			double zcurr = ZLowerBound;

			for (int z = 0; z < Height; ++z) {
				xcurr = XLowerBound;
				for (int x = 0; x < Width; ++x) {
					double value = 0.0;

					if (!Seemless) {
						value = model[xcurr, zcurr];
					} else {
						double swvalue = model[xcurr, zcurr];
						double sevalue = model[xcurr + xextent, zcurr];
						double nwvalue = model[xcurr, zcurr + zextent];
						double nevalue = model[xcurr + xextent, zcurr + zextent];

						double xblend = 1.0 - ((xcurr - XLowerBound) / xextent);
						double zblend = 1.0 - ((zcurr - ZLowerBound) / zextent);
						double z0 = Math.Lerp(swvalue, sevalue, xblend);
						double z1 = Math.Lerp(nwvalue, nevalue, xblend);
						value = Math.Lerp(z0, z1, zblend);
					}

					Destination[x, z] = value;
					xcurr += xdelta;
				}
				zcurr += zdelta;
			}
		}

	}

	public class CylindricalNoiseMapBuilder : NoiseMapBuilder {

		public double LowerAngleBound { get; set; }
		public double UpperAngleBound { get; set; }
		public double LowerHeightBound { get; set; }
		public double UpperHeightBound { get; set; }

		public CylindricalNoiseMapBuilder(double lower_angle, double upper_angle, double lower_height, double upper_height) {
			LowerAngleBound = lower_angle;
			UpperAngleBound = upper_angle;
			LowerHeightBound = lower_height;
			UpperHeightBound = upper_height;
		}

		public override void Build() {
			Destination.Resize(Width, Height);

			var model = new CylinderModel(Source);

			double angle_extent = UpperAngleBound - LowerAngleBound;
			double height_extent = UpperHeightBound - LowerHeightBound;
			double xdelta = angle_extent / (double)Width;
			double ydelta = height_extent / (double)Height;
			double curr_angle = LowerAngleBound;
			double curr_height = LowerHeightBound;

			for (int y = 0; y < Height; ++y) {
				curr_angle = LowerAngleBound;
				for (int x = 0; x < Width; ++x) {
					double value = model[curr_angle, curr_height];
					Destination[x, y] = value;
					curr_angle += xdelta;
				}
				curr_height += ydelta;
			}
		}
	}

	public class SphericalNoiseMapBuilder : NoiseMapBuilder {

		public double EasternLongitudinalBound { get; set; }
		public double WesternLongitudinalBound { get; set; }
		public double NorthernLatitudinalBound { get; set; }
		public double SouthernLatitudinalBound { get; set; }

		public SphericalNoiseMapBuilder(double east, double west, double north, double south) {
			EasternLongitudinalBound = east;
			WesternLongitudinalBound = west;
			NorthernLatitudinalBound = north;
			SouthernLatitudinalBound = south;
		}

		public override void Build() {
			Destination.Resize(Width, Height);

			var model = new SphereModel(Source);

			double longitude_extent = EasternLongitudinalBound - WesternLongitudinalBound;
			double latitude_extent = NorthernLatitudinalBound - SouthernLatitudinalBound;
			double xdelta = longitude_extent / (double)Width;
			double ydelta = latitude_extent / (double)Height;
			double curr_longitude = WesternLongitudinalBound;
			double curr_latitude = SouthernLatitudinalBound;

			for (int y = 0; y < Height; ++y) {
				curr_longitude = WesternLongitudinalBound;
				for (int x = 0; x < Width; ++x) {
					double value = model[curr_latitude, curr_longitude];
					Destination[x, y] = value;
					curr_longitude += xdelta;
				}
				curr_latitude += ydelta;
			}
		}

	}

}
