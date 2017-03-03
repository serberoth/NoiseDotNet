using System;

namespace Noise.Module {
    class AbsModule : IModule {
        private IModule source;

        public AbsModule(IModule source) {
            this.source = source;
        }

        public double this[double x, double y, double z] {
            get { return System.Math.Abs(source[x, y, z]); }
        }

    }

}
