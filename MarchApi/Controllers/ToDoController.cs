using MarchApi.Dtos;
using MarchApi.Models;
using MarchApi.Repositories;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        _log.Information($"{nameof(Get)} search:{search.ToJsonString()}");

        // khởi tạo biến response, mặc định là ErrorCode.Unknow 
        var response = new ResponseDto<List<ToDoItem>?>();

        // truy vấn item từ db
        List<ToDoItem>? data = _toDoItemRepository.GetAll(search);
        
        // nếu data rỗng hoặc có dữ liệu thì ErrorCode.Success và gán data vào payload
        // còn nếu null thì sẽ trả về lỗi ko xác định, được xác định ở lúc khởi tạo
        if (data is not null)
        {
            response.Success(data);
        }

        return Ok(response);
    }

    /// <summary> Tạo 1 ToDo item mới </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    public IActionResult Create([FromBody] ToDoItem input)
    {
        // khởi tạo response
        var response = new ResponseDto();

        // thực hiện thêm item
        DbReturn dbReturn = _toDoItemRepository.Insert(input);

        // cập nhật lại response từ kết quả gọi db
        response.SetProperties(dbReturn.Code, dbReturn.Message);

        return Ok(response);
    }

    /// <summary> Cập nhật 1 ToDo item đã tồn tại </summary>
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    public IActionResult Update([FromBody] ToDoItem input)
    {
        var response = new ResponseDto();

        DbReturn dbReturn = _toDoItemRepository.Update(input);

        response.SetProperties(dbReturn.Code, dbReturn.Message);

        return Ok(response);
    }

    /// <summary> Xóa 1 ToDo item đã tồn tại </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ResponseDto), StatusCodes.Status200OK)]
    public IActionResult Delete([FromRoute] long id)
    {
        var response = new ResponseDto();

        DbReturn dbReturn = _toDoItemRepository.Delete(id);

        response.SetProperties(dbReturn.Code, dbReturn.Message);

        return Ok(response);
    }
}
