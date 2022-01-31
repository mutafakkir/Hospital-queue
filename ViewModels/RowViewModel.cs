using System.ComponentModel.DataAnnotations;

namespace hospital.Entity;

public class RowViewModel
{
    [Required(ErrorMessage = "Введите Имя и Фамилию")]
    [Display(Name = "Имя Фамилия")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Введите номер телефона")]
    [RegularExpression(@"^[\+]?(998[-\s\.]?)([0-9]{2}[-\s\.]?)([0-9]{3}[-\s\.]?)([0-9]{2}[-\s\.]?)([0-9]{2}[-\s\.]?)$",
    ErrorMessage = "Формат телефон номера неправельный.")]
    [Display(Name = "Телефон номер")]
    public string Phone { get; set; }
}