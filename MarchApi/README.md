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

## View Database

-   Để mở database SQLite thì có thể dùng tool **DB Browser SQLite**

1. Tải DB Browser SQLite từ link [Link](https://sqlitebrowser.org/dl/)
    - `DB Browser for SQLite - Standard installer for 64-bit Windows` là bản cài ứng dụng vào máy
    - `DB Browser for SQLite - .zip (no installer) for 64-bit Windows` là bản tải về, giải nén và chạy file DB Browser for SQLite.exe
2. Chạy ứng dụng lên sau đó nhấn vào nít "OPEN DATABASE" ở menu trên
3. Từ file explorer mở tới file MarchDb.db của source này
4. (optional) Chuyển qua tab "Browse Data" để xem table và dữ liệu

## References

-   swagger: https://learn.microsoft.com/vi-vn/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio
