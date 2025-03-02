using FluentValidation;

using MarchApi.Enums;

namespace MarchApi.Dtos;

public class ToDoItemDto
{
    public string? Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int? Rate { get; set; }
    public string? Description { get; set; }
    public bool IsDone { get; set; } = false;
    public PriorityEnum? Priority { get; set; } = PriorityEnum.Medium;
}

public class ToDoItemDtoValidator : AbstractValidator<ToDoItemDto>
{
    public ToDoItemDtoValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(p => p.Name).NotNull().NotEmpty().WithMessage("Tên không được để trống");
    }
}
