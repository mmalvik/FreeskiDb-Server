using FreeskiDb.Persistence.Entities;

namespace Test.FreeskiDb.WebApi
{
    internal class SkiBuilder
    {
        private string _brand;
        private string _modelName;
        private int _tipWidth;
        private int _waistWidth;
        private int _tailWidth;
        private int _weight;

        public SkiBuilder WithBrand(string brand)
        {
            _brand = brand;
            return this;
        }

        public SkiBuilder WithModelName(string model)
        {
            _modelName = model;
            return this;
        }


        public SkiBuilder WithTipWidth(int tipWidth)
        {
            _tipWidth = tipWidth;
            return this;
        }

        public SkiBuilder WithWaistWidth(int waistWidth)
        {
            _waistWidth = waistWidth;
            return this;
        }

        public SkiBuilder WithTailWidth(int tailWidth)
        {
            _tailWidth = tailWidth;
            return this;
        }

        public SkiBuilder WithWeight(int weight)
        {
            _weight = weight;
            return this;
        }

        public Ski Build()
        {
            return new Ski
            {
                Brand = _brand,
                ModelName = _modelName,
                TipWidth = _tipWidth,
                WaistWidth = _waistWidth,
                TailWidth = _tailWidth,
                Weight = _weight
            };
        }
    }
}