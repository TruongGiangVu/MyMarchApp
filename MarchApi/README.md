# March API

## Structure

```
- Constants: constant của source
- Controllers: các controller cấu hình các endpoint
- Dtos: các class truyền gửi object
- Enums: Enum :D
- logs: log file
- Models: các class của source
- Properties: Folder cấu hình publish
- Repositories: Folder chứa các service tập trung vào việc gọi database
- Services: Các service, tập trung vào xử lý các logic nghiệp vụ phức tạp
- Settings: class map tới file appsettings
- Shared: các thành phần share chung cho toàn bộ
- Utilities: tích hợp nhiều thứ khác, nói chung là không biết để đâu thì quăng vào đây
```
## References

- swagger: https://learn.microsoft.com/vi-vn/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio