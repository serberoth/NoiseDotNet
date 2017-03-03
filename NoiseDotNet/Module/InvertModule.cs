
namespace Noise.Module {

    public class InvertModule : IModule {

        private IModule source;

        public InvertModule(IModule source) {
            this.source = source;
        }

        public double this[double x, double y, double z] {
            get { return -(source[x, y, z]); }
        }

    }

}
