
namespace Noise.Module {

    public class BlendModule : IModule {

        private IModule left;
        private IModule right;
        private IModule alpha;

        public BlendModule(IModule left, IModule right, IModule alpha) {
            this.left = left;
            this.right = right;
            this.alpha = alpha;
        }

        public double this[double x, double y, double z] {
            get {
                return Math.Lerp(left[x, y, z], right[x, y, z], (alpha[x, y, z] + 1.0) / 2.0);
            }
        }

    }

}
