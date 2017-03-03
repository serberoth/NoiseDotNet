
namespace Noise.Module {

    public class ScaleBiasModule : IModule {

        private IModule source;

        public double Scale { get; set; }
        public double Bias { get; set; }

        public ScaleBiasModule(IModule source, double scale, double bias) {
            this.source = source;
            Scale = scale;
            Bias = bias;
        }

        public double this[double x, double y, double z] {
            get { return source[x, y, z] * Scale + Bias; }
        }

    }

}