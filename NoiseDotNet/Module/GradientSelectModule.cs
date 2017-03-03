
namespace Noise.Module {

    public class GradientSelectModule : IModule {

        private IModule upper;
        private IModule lower;
        private IModule gradient;

        public double UpperBound { get; set; }
        public double LowerBound { get; set; }
        public double Falloff { get; set; }

        public GradientSelectModule(IModule upper, IModule lower, IModule gradient, double upper_bound = 1.0, double lower_bound = -1.0, double falloff = 0.0) {
            this.upper = upper;
            this.lower = lower;
            this.gradient = gradient;
            UpperBound = upper_bound;
            LowerBound = lower_bound;
            Falloff = falloff;
        }

        public double this[double x, double y, double z] {
            get {
                double bound_size = 0.5 * (UpperBound - LowerBound);
                double falloff = (Falloff > bound_size) ? (bound_size) : Falloff;

                double control = gradient[x, y, z];
                if (falloff > 0.0) {
                    if (control < (LowerBound - falloff)) {
                        return lower[x, y, z];
                    } else if (control < (LowerBound + falloff)) {
                        double lower_curve = (LowerBound - falloff);
                        double upper_curve = (LowerBound + falloff);
                        double alpha = Math.CubicSCurve((control - lower_curve) / (upper_curve - lower_curve));

                        return Math.Lerp(lower[x, y, z], upper[x, y, z], alpha);
                    } else if (control < (UpperBound - falloff)) {
                        return upper[x, y, z];
                    } else if (control < (UpperBound + falloff)) {
                        double lower_curve = (UpperBound - falloff);
                        double upper_curve = (UpperBound + falloff);
                        double alpha = Math.CubicSCurve((control - lower_curve) / (upper_curve - lower_curve));

                        return Math.Lerp(upper[x, y, z], lower[x, y, z], alpha);
                    } else {
                        return lower[x, y, z];
                    }
                } else {
                    if (control < LowerBound || control > UpperBound) {
                        return lower[x, y, z];
                    } else {
                        return upper[x, y, z];
                    }
                }
            }
        }

    }

}
