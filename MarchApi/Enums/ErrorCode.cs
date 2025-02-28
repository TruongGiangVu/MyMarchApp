using System.ComponentModel.DataAnnotations;

namespace MarchApi.Enums;

public enum ErrorCode
{
    [Display(Name = "Thành công")] Success = 00, 
    [Display(Name = "Không tìm thấy")] NotFound = 01,
    [Display(Name = "Lỗi database")] Database = 98,
    [Display(Name = "Lỗi không xác định")] Unknow = 99,
}