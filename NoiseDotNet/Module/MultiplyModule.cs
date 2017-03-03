
namespace Noise.Module {

    public class MultiplyModule : IModule {

        private IModule left;
        private IModule right;

        public MultiplyModule(IModule left, IModule right) {
            this.left = left;
            this.right = right;
        }

        public double this[double x, double y, double z] {
            get { return left[x, y, z] * right[x, y, z]; }
        }

    }

}
