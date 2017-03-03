
namespace Noise.Module {

    public class CheckerModule : IModule {

        public double this[double x, double y, double z] {
            get {
                int ix = (int) System.Math.Floor(Noise.MakeIntRange(x));
                int iy = (int) System.Math.Floor(Noise.MakeIntRange(y));
                int iz = (int) System.Math.Floor(Noise.MakeIntRange(z));

                return ((ix & 1 ^ iy & 1 ^ iz & 1) == 1) ? -1.0 : 1.0;
            }
        }

    }

}
