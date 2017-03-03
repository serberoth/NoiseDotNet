
namespace Noise.Module {

    public class RotationModule : IModule {

        private IModule source;

        public double XAngle { get; set; }
        public double YAngle { get; set; }
        public double ZAngle { get; set; }

        public RotationModule(IModule source, double xangle, double yangle, double zangle) {
            this.source = source;
            XAngle = xangle;
            YAngle = yangle;
            ZAngle = zangle;
        }

        public double[] Matrix {
            get {
                double xcos = System.Math.Cos(XAngle * Math.DEGS_TO_RADS);
                double ycos = System.Math.Cos(YAngle * Math.DEGS_TO_RADS);
                double zcos = System.Math.Cos(ZAngle * Math.DEGS_TO_RADS);
                double xsin = System.Math.Sin(XAngle * Math.DEGS_TO_RADS);
                double ysin = System.Math.Sin(YAngle * Math.DEGS_TO_RADS);
                double zsin = System.Math.Sin(ZAngle * Math.DEGS_TO_RADS);

                return new double[] {
                    ysin * xsin * zsin + ycos * zcos,
                    xcos * zsin,
                    ysin * zcos - ycos * xsin * zsin,
                    ysin * xsin * zcos - ycos * zsin,
                    xcos * zcos,
                    -ycos * xsin * zcos - ysin * zsin,
                    -ysin * xcos,
                    xsin,
                    ycos * xcos,
                };
            }
        }

        public double this[double x, double y, double z] {
            get {
                double[] matrix = Matrix;
                double nx = (matrix[0] * x) + (matrix[1] * y) + (matrix[2] * z);
                double ny = (matrix[3] * x) + (matrix[4] * y) + (matrix[5] * z);
                double nz = (matrix[6] * x) + (matrix[7] * y) + (matrix[8] * z);

                return source[nx, ny, nz];
            }
        }

    }

}
