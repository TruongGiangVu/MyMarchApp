using MarchApi.Dtos;
using MarchApi.Enums;
using MarchApi.Models;
using MarchApi.Repositories.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SQLitePCL;

namespace MarchApi.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class ToDoController : ControllerBase
{
    private readonly Serilog.ILogger _log = Log.ForContext<ToDoController>();
    private readonly IToDoItemRepository _toDoItemRepository;

    public ToDoController(IToDoItemRepository toDoItemRepository)
    {
        _toDoItemRepository = toDoItemRepository;
    }

    /// <summary> Truy vấn danh sách todo item </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDto<List<ToDoItem>?>), StatusCodes.Status200OK)]
    public IActionResult Get([FromQuery] ToDoItemSearchDto search)
    {
        // khởi tạo biến response, mặc định là ErrorCode.UnKnow 
        var response = new ResponseDto<List<ToDoItem>?>();

        // truy vấn item từ db
        List<ToDoItem>? data = _toDoItemRepository.GetAll(search);

        // nếu data rỗng hoặc có dữ liệu thì ErrorCode.Success và gán data vào payload
        // còn nếu null thì sẽ trả về lỗi ko xác định, được xác định ở lúc khởi tạo
        if (data is not null)
        {
            response.Success(data);
        }

        _log.Information($"{nameof(Get)} response: {response.ToLogString()}");
        return Ok(response);
    }

    /// <summary> Truy vấn todo item theo id </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ResponseDto<ToDoItem?>), StatusCodes.Status200OK)]
    public IActionResult GetById([FromRoute] string? id = null)
    {
        // khởi tạo biến response, mặc định là ErrorCode.UnKnow 
        var response = new ResponseDto<ToDoItem?>();

        if (id is null)
        {
            response.SetProperties(ErrorCode.Required, $"Bắt buộc phải điền id");
        }
        else
        {
            // truy vấn item từ db theo id
            ToDoItem? data = _toDoItemRepository.GetById(id);

            // nếu data có dữ liệu thì ErrorCode.Success và gán data vào payload
            if (data is not null)
            {
                response.Success(data);
            }
            else  // còn nếu null trả về lỗi not found
            {
                response.SetProperties(ErrorCode.NotFound, $"Không tìm thấy id {id} này");
            }
        }

        _log.Information($"{nameof(GetById)} response: {response.ToJsonString()}");
        return Ok(response);
    }

    /// <summary> Tạo 1 ToDo item mới </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    public IActionResult Create([FromBody] ToDoItemDto input)
    {
        // khởi tạo response
        var response = new ResponseDto();
        _log.Information($"{nameof(Create)} input: {input.ToJsonString()}");

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


        _log.Information($"{nameof(Create)} response: {response.ToJsonString()}");
        return Ok(response);
    }

    /// <summary> Cập nhật 1 ToDo item đã tồn tại </summary>
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    public IActionResult Update([FromBody] ToDoItemDto input)
    {
        var response = new ResponseDto();
        _log.Information($"{nameof(Update)} input: {input.ToJsonString()}");

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

        _log.Information($"{nameof(Update)} response: {response.ToJsonString()}");
        return Ok(response);
    }

    /// <summary> Xóa 1 ToDo item đã tồn tại </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    public IActionResult Delete([FromRoute] string id)
    {
        var response = new ResponseDto();

        DbReturn dbReturn = _toDoItemRepository.Delete(id);

        response.SetProperties(dbReturn.Code, dbReturn.Message);

        _log.Information($"{nameof(Delete)} response: {response.ToJsonString()}");
        return Ok(response);
    }

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
}
