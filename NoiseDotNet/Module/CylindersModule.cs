
namespace Noise.Module {

    public class CylindersModule : IModule {

        public double Frequency { get; set; }

        public CylindersModule(double frequency) {
            Frequency = frequency;
        }

        public double this[double x, double y, double z] {
            get {
                x *= Frequency;
                z *= Frequency;

                double centerDist = System.Math.Sqrt(x * x + z * z);
                double smallDist = centerDist - System.Math.Floor(centerDist);
                double largeDist = 1.0 - smallDist;
                double nearDist = (smallDist < largeDist) ? smallDist : largeDist;

                return 1.0 - (nearDist * 4.0);
            }
        }

    }

}
