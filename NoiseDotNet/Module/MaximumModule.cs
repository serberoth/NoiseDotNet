
namespace Noise.Module {

    public class MaximumModule : IModule {

        private IModule left;
        private IModule right;

        public MaximumModule(IModule left, IModule right) {
            this.left = left;
            this.right = right;
        }

        public double this[double x, double y, double z] {
            get {
                double l = left[x, y, z];
                double r = right[x, y, z];

                return (l > r) ? l : r;
            }
        }

    }

}