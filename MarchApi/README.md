# March API

## General

-   dotnet core 8.0
-   dotnet sdk >= 8.0.200
-   tool: Visual Studio Code, Visual Studio Community 2022
-   Entity framework core cho kết nối database

## System

-   Hiện đang sử dụng SQLite cho Database
-   File appsettings: Hiện tại xem ở file `appsettings.Development.json` để xem cấu hình môi trường develop
-   User đăng nhập
    -   User role `Admin`: admin/123456
    -   User role `User`: giang/123456

## Structure

```
- Constants: constant của source (được import global trong file "GlobalUsing.cs")
- Controllers: các controller cấu hình các endpoint
- Dtos: các class truyền gửi object
- Enums: Enum :D
- logs: log file
- Models: các class của source
- Properties: Folder cấu hình publish (tạm thời chưa dùng tới)
- Repositories: Các service tập trung vào việc gọi database
- Services: Các service tập trung vào xử lý các logic nghiệp vụ phức tạp và gọi tới Repositories
- Settings: class map tới file appsettings
- Shared: các thành phần share chung cho toàn bộ source (được import global trong file "GlobalUsing.cs")
- Utilities: Folder đa dụng, tích hợp nhiều thứ khác, nói chung là không biết để đâu thì quăng vào đây
- GlobalUsing.cs: File global using cho toàn bộ source (khỏi mất công using nhiều lần :v)
```

## View Database

-   Để mở database SQLite thì có thể dùng tool **DB Browser SQLite**

1. Tải DB Browser SQLite từ [Link](https://sqlitebrowser.org/dl/)
    - `DB Browser for SQLite - Standard installer for 64-bit Windows` là bản cài ứng dụng vào máy
    - `DB Browser for SQLite - .zip (no installer) for 64-bit Windows` là bản tải về, giải nén và chạy file `DB Browser for SQLite.exe`
2. Chạy ứng dụng lên sau đó nhấn vào nít "OPEN DATABASE" ở menu trên
3. Từ file explorer mở tới file `MarchDb.db` của source này, khi này đã mở database lên
4. (optional) Chuyển qua tab "Browse Data" để xem table và dữ liệu

## References

-   swagger hiển thị summary: https://learn.microsoft.com/vi-vn/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio
-   swagger hiển thị ổ khoá với jwt bearer: https://code-maze.com/swagger-authorization-aspnet-core/
