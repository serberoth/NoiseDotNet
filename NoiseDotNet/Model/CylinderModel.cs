
namespace Noise.Model {

    public class CylinderModel {

        private IModule module;

        public CylinderModel(IModule module) {
            this.module = module;
        }

        public double this[double angle, double height] {
            get {
                double x = System.Math.Cos(angle * Math.DEGS_TO_RADS);
                double y = height;
                double z = System.Math.Sin(angle * Math.DEGS_TO_RADS);

                return module[x, y, z];
            }
        }

    }

}