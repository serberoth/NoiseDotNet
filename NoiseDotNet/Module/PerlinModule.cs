
namespace Noise.Module {

    public class PerlinModule : AbstractNoiseModule {

        public PerlinModule(double frequency = 1.0, double lacunarity = 2.0, int octaves = 6, double persistance = 0.5, int seed = 0, Quality quality = Quality.NORMAL) {
            Frequency = frequency;
            Lacunarity = lacunarity;
            Octaves = octaves;
            Persistance = persistance;
            Seed = seed;
            Quality = quality;
        }

        public override double this[double x, double y, double z] {
            get {
                double val = 0.0;
                double signal = 0.0;
                double persistance = 1.0;
                double nx, ny, nz;
                int seed;

                x *= Frequency;
                y *= Frequency;
                z *= Frequency;

                for (int octave = 0; octave < Octaves; ++octave) {
                    nx = Noise.MakeIntRange(x);
                    ny = Noise.MakeIntRange(y);
                    nz = Noise.MakeIntRange(z);

                    seed = (int) ((Seed + octave) & 0xffffffff);
                    signal = Noise.CoherentGradientNoise3D(nx, ny, nz, seed, Quality);
                    val += signal * persistance;

                    x *= Lacunarity;
                    y *= Lacunarity;
                    z *= Lacunarity;
                    persistance *= Persistance;
                }

                return val;
            }
        }

    }

}
