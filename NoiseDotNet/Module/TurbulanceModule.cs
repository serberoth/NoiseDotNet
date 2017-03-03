
namespace Noise.Module {

    public class TurbulanceModule : IModule {

        private IModule source;
        private PerlinModule xmodule;
        private PerlinModule ymodule;
        private PerlinModule zmodule;

        public double Power { get; set; }
        public int Roughness {
            get { return xmodule.Octaves; }
            set {
                xmodule.Octaves = value;
                ymodule.Octaves = value;
                zmodule.Octaves = value;
            }
        }

        public double Frequencey {
            get { return xmodule.Frequency; }
            set {
                xmodule.Frequency = value;
                ymodule.Frequency = value;
                zmodule.Frequency = value;
            }
        }

        public int Seed {
            get { return xmodule.Seed; }
            set {
                xmodule.Seed = value;
                ymodule.Seed = value + 1;
                zmodule.Seed = value + 2;
            }
        }

        public TurbulanceModule(IModule source, double power, int roughness, double frequency, int seed) {
            this.source = source;
            this.xmodule = new PerlinModule();
            this.ymodule = new PerlinModule();
            this.zmodule = new PerlinModule();
            
            Power = power;
            Roughness = roughness;
            Frequencey = frequency;
            Seed = seed;
        }

        public double this[double x, double y, double z] {
            get {
                double x0 = x + (12414.0 / 65536.0);
                double y0 = y + (65124.0 / 65536.0);
                double z0 = z + (31337.0 / 65536.0);
                double x1 = x + (26519.0 / 65536.0);
                double y1 = y + (18128.0 / 65536.0);
                double z1 = z + (60493.0 / 65536.0);
                double x2 = x + (53820.0 / 65536.0);
                double y2 = y + (11213.0 / 65536.0);
                double z2 = z + (44845.0 / 65536.0);

                double xdisplace = x + (xmodule[x0, y0, z0] * Power);
                double ydisplace = y + (ymodule[x1, y1, z1] * Power);
                double zdisplace = z + (zmodule[x2, y2, z2] * Power);

                return source[xdisplace, ydisplace, zdisplace];
            }
        }

    }

}
