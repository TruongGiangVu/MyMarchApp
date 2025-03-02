using MarchApi.Dtos;
using MarchApi.Enums;
using MarchApi.Models;
using MarchApi.Repositories.Interfaces;
using MarchApi.Services.Interfaces;

namespace MarchApi.Services.Implements;

public class ToDoService : IToDoService
{
    private readonly ITagRepository _tagRepository;
    private readonly IToDoItemRepository _toDoItemRepository;
    private readonly IToDoCheckListRepository _toDoCheckListRepository;

    public ToDoService(IToDoItemRepository toDoItemRepository,
                        ITagRepository tagRepository,
                        IToDoCheckListRepository toDoCheckListRepository)
    {
        _tagRepository = tagRepository;
        _toDoItemRepository = toDoItemRepository;
        _toDoCheckListRepository = toDoCheckListRepository;
    }

    #region Public
    public ResponseDto CreateToDoItem(ToDoItemDto input)
    {
        var response = new ResponseDto();

        (bool isValid, string message) = ValidateInput(input);
        if (isValid == false) // nếu dữ liệu validate sai đặc tả
        {
            response.SetProperties(ErrorCode.Invalid, message);
        }
        else // nếu validate pass
        {
            // thực hiện thêm item
            DbReturn dbReturn = _toDoItemRepository.Insert(input);

            // cập nhật lại response từ kết quả gọi db
            response.SetProperties(dbReturn.Code, dbReturn.Message);
        }

        return response;
    }

    public ResponseDto UpdateToDoItem(ToDoItemDto input)
    {
        var response = new ResponseDto();

        (bool isValid, string message) = ValidateInput(input);
        if (isValid == false) // nếu dữ liệu validate sai đặc tả
        {
            response.SetProperties(ErrorCode.Invalid, message);
        }
        else // nếu validate pass
        {
            // thực hiện thêm item
            DbReturn dbReturn = _toDoItemRepository.Update(input);

            // cập nhật lại response từ kết quả gọi db
            response.SetProperties(dbReturn.Code, dbReturn.Message);
        }

        return response;
    }

    #endregion Public


    #region Private

    /// <summary> Kiểm tra dữ liệu ToDoItemDto </summary>
    private static (bool isValid, string message) ValidateInput(ToDoItemDto input)
    {
        bool isValid = true;
        string message = string.Empty;

        if (string.IsNullOrWhiteSpace(input.Name)) // nếu name rỗng
        {
            isValid = false;
            message = "Tên không được để trống";
        }
        else if (input.Priority is null) // nếu Priority bằng null
        {
            isValid = false;
            message = "Độ ưu tiên không được để trống";
        }
        return (isValid, message);

    }
    #endregion Private
}
