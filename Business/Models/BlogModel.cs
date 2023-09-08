#nullable disable



using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class BlogModel : RecordBase
    {

        #region Entity'den gelenler

        [Required(ErrorMessage = "{0} is required!")]
        [MinLength(3, ErrorMessage = "{0} must be minimum {1} characters!")] 
        [MaxLength(150, ErrorMessage = "{0} must be maximum {1} characters!")] 
        [DisplayName("Blog Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(300, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        [Range(1, 5, ErrorMessage = "{0} must be between {1} and {2}!")]
        public byte? Score { get; set; }

        [DisplayName("User")]
        [Required(ErrorMessage = "{0} is required!")]
        public int? UserId { get; set; }

        #endregion

        #region View'larda Gösterim veya Veri Girişi için Kullanacağımız Özellikler
        [DisplayName("Create Date")]
        public string CreateDateDisplay { get; set; }

        [DisplayName("Update Date")]
        public string UpdateDateDisplay { get; set; }

        [DisplayName("User")]
        public string UserNameDisplay { get; set; }

		[DisplayName("Score")]
		public string ScoreDisplay { get; set; }

		[DisplayName("Tags")]
        public List<TagModel> TagsDisplay { get; set; }

        [DisplayName("Tags")]
        [Required(ErrorMessage = "{0} are required!")]
        public List<int> TagIds { get; set; }

        #endregion

        /*
        Entity ve model özelliklerinde kullanılabilecek bazı genel data annotation'lar (attribute): 
        NOT: Data annotation'lar ile sadece model verisi üzerinden basit validasyonlar yapılabilir, örneğin veritabanındaki bir tablo üzerinden 
        validasyon gerekiyorsa bu validasyon service class'larında yapılmalıdır.

        Key (Entity): Özelliğin birincil anahtar olduğunu belirtir ve veritabanı oluşturulurken tablodaki sütun karşılığı otomatik artan sayı olarak ayarlanır.
        Required (Entity ve Model): Özelliğin zorunlu olduğunu belirtir.
        Column (Entity): Özelliğin veritabanı tablosundaki sütunu ile ilgili ayarlarını belirtir, örneğin sütun adı (Name), sütun veri tipi (TypeName) ve sütun sırası (Order: çoklu key için kullanılır).
        DataType (Model): Özelliğin veri tipi için kullanılır, örneğin Text, Date, Time, DateTime, Currency, EmailAddress, PhoneNumber, Password, v.b.
        ReadOnly (Model): Özelliğin sadece okunabilir olması için kullanılır.
        DisplayFormat (Model): Metinsel veri gösteriminde kullanılacak format'ı belirtir ve genellikle tarih, ondalık sayı, v.b. formatlama işlemleri için kullanılır.
        Table (Entity): Veritabanında oluşacak tablonun adını (Name) değiştirmek için kullanılır.
        StringLength (Entity ve Model): Metinsel tipte özellikler için girilecek karakter sayısının maksimumunu belirtmede kullanılır.
        MinLength (Model): Metinsel tipte özellikler için girilecek karakter sayısının minimumunu belirtmede kullanılır.
        MaxLength (Model): Metinsel tipte özellikler için girilecek karakter sayısının maksimumunu belirtmede kullanılır.
        Compare (Model): Tanımlandığı özelliğin başka bir özellik üzerinden verilerinin karşılaştırılması için kullanılır.
        RegularExpression (Model): Verilerin daha detaylı validasyonu için öğrenilip kullanılabilecek bir doğrulama desenidir.
        Range (Model): Sayısal değerler için aralık belirtmede kullanılır.
        EmailAddress (Model): Özellik verisinin e-posta formatında olması için kullanılır.
        Phone (Model): Özellik verisinin telefon formatında olması için kullanılır.
        NotMapped (Entity): Özelliğin veritabanında ilgili tablosunda sütununun oluşturulmaması için kullanılır.
        JsonIgnore (Model): Özelliğin oluşturulacak JSON formatındaki veriye dahil edilmemesini sağlar.
        */


    }
}
