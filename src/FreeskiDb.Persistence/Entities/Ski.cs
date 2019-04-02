namespace FreeskiDb.Persistence.Entities
{
    /// <summary>
    /// Represents a ski to be stored somewhere.
    /// </summary>
    public class Ski
    {
        /// <summary>
        /// Which brand the skis is from, e.g. K2 og Atomic.
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// The model name of this ski.
        /// </summary>
        public string ModelName { get; set; }

        /// <summary>
        /// The width of the ski at the tip/front. Unit is millimeters.
        /// </summary>
        public int TipWidth { get; set; }

        /// <summary>
        /// The width of the ski at the waist/middle. Unit is millimeters.
        /// </summary>
        public int WaistWidth { get; set; }

        /// <summary>
        /// The width of the ski at the tail/rear. Unit is millimeters.
        /// </summary>
        public int TailWidth { get; set; }

        /// <summary>
        /// Weight of a single ski. Unit is grams.
        /// </summary>
        public int Weight { get; set; }
    }
}