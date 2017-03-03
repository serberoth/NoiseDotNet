
namespace Noise.Module {

    public class RidgedMultiModule : AbstractNoiseModule {

        public RidgedMultiModule(double frequency = 1.0, double lacunarity = 2.0, int octaves = 6, int seed = 0, Quality quality = Quality.NORMAL) {
            Frequency = frequency;
            Lacunarity = lacunarity;
            Octaves = octaves;
            Seed = seed;
            Quality = quality;
        }

        public double H { get; set; }

        public RidgedMultiModule() {
            H = 1.0;
        }

        private double[] SpectralWeights {
            get {
                // double h = 1.0;
                double[] weights = new double[Octaves];
                double frequency = 1.0;

                for (int i = 0; i < Octaves; ++i) {
                    weights[i] = System.Math.Pow(frequency, -H);
                    frequency *= Lacunarity;
                }
                return weights;
            }
        }

        public override double this[double x, double y, double z] {
            get {
                x *= Frequency;
                y *= Frequency;
                z *= Frequency;

                double signal = 0.0;
                double val = 0.0;
                double weight = 1.0;

                double offset = 1.0;
                double gain = 2.0;

                double[] weights = SpectralWeights;

                for (int octave = 0; octave < Octaves; ++octave) {
                    double nx = Noise.MakeIntRange(x);
                    double ny = Noise.MakeIntRange(y);
                    double nz = Noise.MakeIntRange(z);

                    int seed = (Seed + octave) & 0x7fffffff;
                    signal = Noise.CoherentGradientNoise3D(nx, ny, nz, seed, Quality);

                    signal = System.Math.Abs(signal);
                    signal = offset - signal;
                    signal *= signal;
                    signal *= weight;

                    weight = signal * gain;
                    if (weight > 1.0) {
                        weight = 1.0;
                    }
                    if (weight < 0.0) {
                        weight = 0.0;
                    }

                    val += (signal * weights[octave]);

                    x *= Lacunarity;
                    y *= Lacunarity;
                    z *= Lacunarity;
                }

                return (val * 1.25) - 1.0;
            }
        }
    }
    
}
