
namespace Noise.Module {

    public class VoronoiModule : IModule {

        private static readonly double SQRT_3 = System.Math.Sqrt(3.0);

        public double Displacement { get; set; }
        public bool EnableDistance { get; set; }
        public double Frequency { get; set; }
        public int Seed { get; set; }

        public VoronoiModule(double displacement = 1.0, bool enableDistance = false, double frequency = 1.0, int seed = 0) {
            Displacement = displacement;
            EnableDistance = enableDistance;
            Frequency = frequency;
            Seed = seed;
        }

        public double this[double x, double y, double z] {
            get {
                x *= Frequency;
                y *= Frequency;
                z *= Frequency;

                int ix = (x > 0.0 ? (int) x : (int) x - 1);
                int iy = (y > 0.0 ? (int) y : (int) y - 1);
                int iz = (z > 0.0 ? (int) z : (int) z - 1);

                double min_dist = 2147483647.0;
                double xs = 0.0, ys = 0.0, zs = 0.0;
                
                for (int k = iz - 2; k <= iz + 2; ++k) {
                    for (int j = iy - 2; j <= iy + 2; ++j) {
                        for (int i = ix - 2; i <= ix + 2; ++i) {
                            double xpos = ix + Noise.ValueNoise3D(i, j, k, Seed + 0);
                            double ypos = iy + Noise.ValueNoise3D(i, j, k, Seed + 1);
                            double zpos = iz + Noise.ValueNoise3D(i, j, k, Seed + 2);
                            double xdist = xpos - x;
                            double ydist = ypos - y;
                            double zdist = zpos - z;
                            double dist = xdist * xdist + ydist * ydist + zdist * zdist;

                            if (dist < min_dist) {
                                min_dist = dist;

                                xs = xpos;
                                ys = ypos;
                                zs = zpos;
                            }
                        }
                    }
                }

                double val = 0.0;
                if (EnableDistance) {
                    double xdist = xs - x;
                    double ydist = ys - y;
                    double zdist = zs - z;
                    val = System.Math.Sqrt(xdist * xdist + ydist * ydist + zdist * zdist) * SQRT_3 - 1.0;
                } else {
                    val = 0.0;
                }

                return val + (Displacement * (double) Noise.ValueNoise3D(
                    (int) (System.Math.Floor(xs)),
                    (int) (System.Math.Floor(ys)),
                    (int) (System.Math.Floor(zs)), 0));
            }
        }

    }

}
