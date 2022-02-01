using System.ComponentModel.DataAnnotations;

namespace hospital.ViewModels;

public class RowViewModel
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [Required(ErrorMessage = "Введите Имя и Фамилию")]
    [Display(Name = "Имя Фамилия")]
    public string Fullname { get; set; }

    [Required(ErrorMessage = "Введите номер телефона")]
    [RegularExpression(@"^[\+]?(998[-\s\.]?)([0-9]{2}[-\s\.]?)([0-9]{3}[-\s\.]?)([0-9]{2}[-\s\.]?)([0-9]{2}[-\s\.]?)$",
    ErrorMessage = "Формат телефон номера неправельный.")]
    [Display(Name = "Телефон номер")]
    public string Phone { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ModifiedAt { get; set; }
    public DateTimeOffset RowTime { get; set; }
    public bool IsActive { get; set; }
}