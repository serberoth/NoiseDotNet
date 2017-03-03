
namespace Noise.Utility {
	
	public sealed class Renderer {
		
		Renderer() {
		}

		public static Colour[] RenderImage(NoiseMap source, Gradient gradient, Colour[] background = null) {
			int width = source.Width;
			int height = source.Height;

			Colour[] dest = new Colour[width * height];
			System.Diagnostics.Debug.Assert(background.Length == dest.Length);

			for (int y = 0; y < height; ++y) {
				for (int x = 0; x < width; ++x) {
					Colour fore = gradient[source[x, y]];

					Colour back = new Colour(1.0, 1.0, 1.0, 1.0);
					if (background != null) {
						back = background[y * width + x];
					}

					dest[y * width + x] = Blend(fore, back, 1.0);
				}
			}

			return dest;
		}

		private static Colour Blend(Colour foreground, Colour background, double intensity) {
			double r = Math.Lerp(background.R, foreground.R, foreground.A);
			double g = Math.Lerp(background.G, foreground.G, foreground.A);
			double b = Math.Lerp(background.B, foreground.B, foreground.A);

			r = Math.Clamp(r, 0.0, 1.0);
			g = Math.Clamp(g, 0.0, 1.0);
			b = Math.Clamp(b, 0.0, 1.0);

			return new Colour(r, g, b, System.Math.Max(foreground.A, background.A));
		}

		public static Colour[] RenderNormalMap(NoiseMap source, double bump_height, bool wrap = false) {
			int width = source.Width;
			int height = source.Height;

			Colour[] dest = new Colour[width * height];

			for (int y = 0; y < height; ++y) {
				for (int x = 0; x < width; ++x) {
					int x_right_offset, y_up_offset;
					if (wrap) {
						if (x == width - 1) {
							x_right_offset = -(width - 1);
						} else {
							x_right_offset = 1;
						}

						if (y == height - 1) {
							y_up_offset = -(height - 1);
						} else {
							y_up_offset = 1;
						}
					} else {
						if (x == width - 1) {
							x_right_offset = 0;
						} else {
							x_right_offset = 1;
						}
						if (y == height - 1) {
							y_up_offset = 0;
						} else {
							y_up_offset = 1;
						}
					}
					y_up_offset *= source.Width;

					double nc = source[x, y];
					double nr = source[x + x_right_offset, y];
					double nu = source[x, y + y_up_offset];

					dest[y * width + x] = NormalColour(nc, nr, nu, bump_height);
				}
			}

			return dest;
		}

		private static Colour NormalColour(double nc, double nr, double nu, double bump_height) {
			nc *= bump_height;
			nr *= bump_height;
			nu *= bump_height;

			double ncr = (nc - nr);
			double ncu = (nc - nu);

			double d = System.Math.Sqrt((ncu * ncu) + (ncr * ncr) + 1);
			double vxc = (nc - nr) / d;
			double vyc = (nc - nu) / d;
			double vzc = 1.0 / d;

			double xc = System.Math.Floor(vxc + 1.0);
			double yc = System.Math.Floor(vyc + 1.0);
			double zc = System.Math.Floor(vzc + 1.0);

			return new Colour(xc, yc, zc, 0.0);
		}

	}

}
