
namespace Noise.Module {

    public class MinimumModule : IModule {

        public IModule left;
        public IModule right;

        public MinimumModule(IModule left, IModule right) {
            this.left = left;
            this.right = right;
        }

        public double this[double x, double y, double z] {
            get {
                double l = left[x, y, z];
                double r = right[x, y, z];

                return (l < r) ? l : r;
            }
        }

    }

}
