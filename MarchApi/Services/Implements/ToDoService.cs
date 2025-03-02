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


}
