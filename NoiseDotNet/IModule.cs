
namespace Noise {

    public enum Quality {
        LOW,
        NORMAL,
        HIGH,
    }

    public interface IModule {

        double this[double x, double y, double z] { get; }

    }

    public abstract class AbstractNoiseModule : IModule {
        public double Frequency { get; set; }
        public double Lacunarity { get; set; }
        public int Octaves { get; set; }
        public double Persistance { get; set; }
        public int Seed { get; set; }

        public Quality Quality { get; set; }

        public abstract double this[double x, double y, double z] { get; }

    }


}
