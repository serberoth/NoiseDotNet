
namespace Noise.Module {

    public class PowerModule : IModule {

        private IModule principal;
        private IModule exponent;

        public PowerModule(IModule principal, IModule exponent) {
            this.principal = principal;
            this.exponent = exponent;
        }

        public double this[double x, double y, double z] {
            get {
                return System.Math.Pow(principal[x, y, z], exponent[x, y, z]);
            }
        }

    }

}
