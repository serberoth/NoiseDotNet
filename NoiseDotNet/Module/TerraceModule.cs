using System.Collections.Generic;

namespace Noise.Module {

    public class TerraceModule : IModule {

        private IModule source;
        private List<double> controlPoints;

        public bool Invert { get; set; }

        public TerraceModule(IModule source, int count) {
			this.source = source;
            BuildControlPoints(count);
        }

        public void Add(double point) {
            controlPoints.Add(point);
            controlPoints.Sort();
        }

        public int Count { get { return controlPoints.Count; } }

        public void Clear() {
            controlPoints.Clear();
        }

        public void BuildControlPoints(int count) {
            if (count < 2) {
                throw new System.ArgumentException("" + count);
            }

            controlPoints.Clear();

            double step = 2.0 / ((double) count - 1.0);
            double curr = -1.0;
            for (int i = 0; i < count; ++i) {
                controlPoints.Add(curr);
                curr += step;
            }

            controlPoints.Sort();
        }

        public double this[double x, double y, double z] {
            get {
                double source_value = source[x, y, z];

                int index;
                for (index = 0; index < controlPoints.Count; ++index) {
                    if (source_value < controlPoints[index]) {
                        break;
                    }
                }

                int index0 = Math.Clamp(index - 1, 0, controlPoints.Count - 1);
                int index1 = Math.Clamp(index - 0, 0, controlPoints.Count - 1);

                if (index0 == index1) {
                    return controlPoints[index0];
                }

                double value0 = controlPoints[index0];
                double value1 = controlPoints[index1];
                double alpha = (source_value - value0) / (value1 - value0);

                if (Invert) {
                    alpha = 1.0 - alpha;
                    double temp = value0;
                    value0 = value1;
                    value1 = temp;
                }

                alpha *= alpha;

                return Math.Lerp(value0, value1, alpha);
            }
        }

    }

}
