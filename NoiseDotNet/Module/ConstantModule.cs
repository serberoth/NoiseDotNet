
namespace Noise.Module {

    public class ConstantModule : IModule {

        private double Value { get; set; }

        public ConstantModule(double value) {
            Value = value;
        }

        public double this[double x, double y, double z] {
            get { return Value; }
        }
        
    }

}
