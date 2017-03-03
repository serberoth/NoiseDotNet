
namespace Noise.Model {

    public class SphereModel {

        private IModule module;

        public SphereModel(IModule module) {
            this.module = module;
        }

        public double this[double latitude, double longitude] {
            get {
                double r = System.Math.Cos(latitude * Math.DEGS_TO_RADS);
                double x = r * System.Math.Cos(longitude * Math.DEGS_TO_RADS);
                double y = r * System.Math.Sin(latitude * Math.DEGS_TO_RADS);
                double z = r * System.Math.Sin(longitude * Math.DEGS_TO_RADS);

                return module[x, y, z];
            }
        }

    }

}
