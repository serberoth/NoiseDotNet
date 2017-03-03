
namespace Noise.Module {

    public class ClampModule : IModule {

        private IModule source;

        public double Lower { get; set; }
        public double Upper { get; set; }

        public ClampModule(IModule source, double lower, double upper) {
            this.source = source;
            Lower = lower;
            Upper = upper;
        }

        public double this[double x, double y, double z] {
            get {
                System.Diagnostics.Debug.Assert(Lower < Upper);

                double val = source[x, y, z];
                if (val < Lower) {
                    return Lower;
                } else if (val > Upper) {
                    return Upper;
                }
                return val;
            }
        }

    }

}
