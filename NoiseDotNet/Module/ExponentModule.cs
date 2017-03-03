
namespace Noise.Module {

    public class ExponentModule : IModule {

        private IModule source;

        public double Exponent { get; set; }

        public ExponentModule(IModule source, double exponent) {
            this.source = source;
            Exponent = exponent;
        }

        public double this[double x, double y, double z] {
            get {
                double val = source[x, y, z];
                return System.Math.Pow(System.Math.Abs((val + 1.0) / 2.0), Exponent) * 2.0 - 1.0;
            }
        }

    }

}
