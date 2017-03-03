
namespace Noise.Model {

    public class LineModel {

        private IModule module;

        public double X0 { get; set; }
        public double X1 { get; set; }
        public double Y0 { get; set; }
        public double Y1 { get; set; }
        public double Z0 { get; set; }
        public double Z1 { get; set; }

        public bool Attenuate { get; set; }

        public LineModel(IModule module, bool attenuate = true, double x0 = 0.0, double x1 = 1.0, double y0 = 0.0, double y1 = 1.0, double z0 = 0.0, double z1 = 1.0) {
            this.module = module;
            Attenuate = attenuate;
            X0 = x0;        X1 = x1;
            Y0 = y0;        Y1 = y1;
            Z0 = z0;        Z1 = z1;
        }

        public double this[double t] {
            get {
                double x = (X1 - X0) * t + X0;
                double y = (Y1 - Y0) * t + Y0;
                double z = (Z1 - Z0) * t + Z0;
                double val = module[x, y, z];

                if (Attenuate) {
                    return t * (1.0 - t) * 4 * val;
                }

                return val;
            }
        }

    }

}
