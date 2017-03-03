
namespace Noise.Module {

    public class TranslateModule : IModule {

        private IModule source;

        public double XTranslation { get; set; }
        public double YTranslation { get; set; }
        public double ZTranslation { get; set; }

        public TranslateModule(IModule source, double xtranslation, double ytranslation, double ztranslation) {
            this.source = source;
            XTranslation = xtranslation;
            YTranslation = ytranslation;
            ZTranslation = ztranslation;
        }

        public double this[double x, double y, double z] {
            get {
                return source[x + XTranslation, y + YTranslation, z + ZTranslation];
            }
        }

    }

}
