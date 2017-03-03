
namespace Noise.Utility {
	
	public class NoiseMap {

		private double[] map;

		public double BorderValue { get; set; }

		public int Width { get; private set; }
		public int Height { get; private set; }

		public NoiseMap(int width, int height) {
			Width = width;
			Height = height;
			map = new double[Width * Height];
		}

		public double this[int x, int y] {
			get {
				if (map != null && x >= 0 && x < Width && y >= 0 && y < Height) {
					return map[y * Width + x];
				}

				return BorderValue;
			}
			set {
				if (map != null && x >= 0 && x < Width && y >= 0 && y < Height) {
					map[y * Width + x] = value;
				}
			}
		}

		public void Clear(double value = 0.0) {
			for (int y = 0; y < Height; ++y) {
				for (int x = 0; x < Width; ++x) {
					map[y * Width + x] = value;
				}
			}
		}

		public void Resize(int w, int h) {
			if (w != Width || h != Height) {
				Width = w;
				Height = h;
				map = new double[Width * Height];
			}
		}

	}

}
