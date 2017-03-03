
namespace Noise.Module {

    public class ScalePointModule : IModule {

        private IModule source;

        public double XScale { get; set; }
        public double YScale { get; set; }
        public double ZScale { get; set; }

        public ScalePointModule(IModule source, double xscale, double yscale, double zscale) {
            this.source = source;
            XScale = xscale;
            YScale = yscale;
            ZScale = zscale;
        }

        public double this[double x, double y, double z] {
            get { return source[x * XScale, y * YScale, z * ZScale]; }
        }

    }

}
