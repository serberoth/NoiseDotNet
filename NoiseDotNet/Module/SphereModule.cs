
namespace Noise.Module {

    public class SphereModule : IModule {

        public double Frequency { get; set; }

        public SphereModule(double frequency) {
            Frequency = frequency;
        }

        public double this[double x, double y, double z] {
            get {
                x *= Frequency;
                y *= Frequency;
                z *= Frequency;

                double center_dist = System.Math.Sqrt(x * x + y * y + z * z);
                double small_sphere = center_dist - System.Math.Floor(center_dist);
                double large_sphere = 1.0 - small_sphere;
                double near_dist = System.Math.Min(small_sphere, large_sphere);
                
                return 1.0 - (near_dist) * 4.0;
            }
        }

    }

}
