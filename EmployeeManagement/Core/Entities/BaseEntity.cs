namespace Core.Entities
{
    /// <summary>
    /// Hər bir database modeli bu sinifdən miras almalıdır
    /// və bu sinifdə hər bir cədvəl üçün istifadə olunan sütunların prop-ları
    /// saxlanılacaqdır
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Sətirin identifikasiya nömrəsi
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Sətirin yaradılma tarixi
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Sətir fiziki olaraq silinmədiyindən silinmiş
        /// sətirin identifikasiya nömrəsi saxlanılır
        /// </summary>
        public int Deleted { get; set; }

    }
}
