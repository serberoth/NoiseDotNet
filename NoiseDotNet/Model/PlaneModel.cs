
namespace Noise.Model {

    public class PlaneModel {

        private IModule module;

        public PlaneModel(IModule module) {
            this.module = module;
        }

        public double this[double x, double z] {
            get {
                return module[x, 0.0, z];
            }
        }
        
    }

}
