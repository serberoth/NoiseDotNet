
namespace Noise.Module {

    public class DisplaceModule : IModule {

        private IModule source;
        private IModule xmodule;
        private IModule ymodule;
        private IModule zmodule;

        public DisplaceModule(IModule source, IModule xmodule, IModule ymodule, IModule zmodule) {
            this.source = source;
            this.xmodule = xmodule;
            this.ymodule = ymodule;
            this.zmodule = zmodule;
        }

        public double this[double x, double y, double z] {
            get {
                double xdisplace = x + xmodule[x, y, z];
                double ydisplace = y + ymodule[x, y, z];
                double zdisplace = z + zmodule[x, y, z];

                return source[xdisplace, ydisplace, zdisplace];
            }
        }

    }

}
