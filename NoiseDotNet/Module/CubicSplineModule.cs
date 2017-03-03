using System;
using System.Collections.Generic;

namespace Noise.Module {

    public class CubicSplineModule : IModule {

        private struct ControlPoint : IComparable<ControlPoint> {
            public double Input { get; set; }
            public double Output { get; set; }

			public ControlPoint(double input, double output) : this() {
                Input = input;
                Output = output;
            }

            public int CompareTo(ControlPoint other) {
                return Input.CompareTo(other.Input);
            }

        }

        private IModule source;
        private List<ControlPoint> controlPoints;

        public CubicSplineModule(IModule source) {
            this.source = source;
        }

        public void Add(double input, double output) {
            controlPoints.Add(new ControlPoint(input, output));
            controlPoints.Sort();
        }

        public void Clear() {
            controlPoints.Clear();
        }

        public int Count {
            get { return controlPoints.Count; }
        }

        public double this[double x, double y, double z] {
            get {
                System.Diagnostics.Debug.Assert(controlPoints.Count > 4);

                double val = source[x, y, z];
                int index = 0;
                for (index = 0; index < controlPoints.Count; ++index) {
                    if (val < controlPoints[index].Input) {
                        break;
                    }
                }

                int index0 = Math.Clamp(index - 2, 0, controlPoints.Count - 1);
                int index1 = Math.Clamp(index - 1, 0, controlPoints.Count - 1);
                int index2 = Math.Clamp(index - 0, 0, controlPoints.Count - 1);
                int index3 = Math.Clamp(index + 1, 0, controlPoints.Count - 1);

                if (index1 == index2) {
                    return controlPoints[index1].Output;
                }

                double input0 = controlPoints[index1].Input;
                double input1 = controlPoints[index2].Input;
                double alpha = (val - input0) / (input1 - input0);

                return Math.CubicInterp(controlPoints[index0].Output, controlPoints[index1].Output,
                        controlPoints[index2].Output, controlPoints[index3].Output, alpha);
            }
        }

    }

}
