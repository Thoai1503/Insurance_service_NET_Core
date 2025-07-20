
**Insurance_agency_NET_Core** is an insurance agency management system developed with ASP.NET Core MVC. It supports managing insurance types, contracts, policies, customers, and provides separate interfaces for Admin and User roles.

## Main Features

- **Insurance Package Management**: Create, edit, delete, and view details of various insurance types (motorbike, truck, life insurance, etc.).
- **Policy Management**: Manage details of policies belonging to each insurance package.
- **Contract Management**: View, create, update, and delete insurance contracts.
- **Role Management**: Assign different permissions for Admin and User.
- **User Interface**: Display list of insurances, detailed view for each package, and purchase contracts.
- **Admin Dashboard**: Separate area for administrators to manage the entire system.

## Technologies Used

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core (EF Core)
- SQL Server

## How to Run the Source Code

### 1. Prerequisites

- .NET 8.0 SDK: https://dotnet.microsoft.com/
- SQL Server (local or cloud)

### 2. Clone the source code

```bash
git clone https://github.com/Thoai1503/Insurance_agancy_NET_Core.git
cd Insurance_agancy_NET_Core
```

### 3. Configure the Database

- Open the `appsettings.json` file
- Update the connection string to match your SQL Server configuration:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD;Trusted_Connection=False;MultipleActiveResultSets=true"
}
```

### 4. Restore and build the project

```bash
dotnet restore
dotnet build
```

### 5. Update the database

If you don't have the database yet, run the migration command (if migrations are created):

```bash
dotnet ef database update
```
> If there are no migrations yet, create the first migration:
>
> ```bash
> dotnet ef migrations add InitialCreate
> dotnet ef database update
> ```

### 6. Run the application

```bash
dotnet run
```

- Access the website at: `http://localhost:5000` or the port shown in the console.

## Notes

- Default Admin/User accounts (if any) can be found in the database or contact the developer.
- To access admin features, log in with an Admin account.

---

**For contributions and feedback, please visit [github.com/Thoai1503/Insurance_agancy_NET_Core](https://github.com/Thoai1503/Insurance_agancy_NET_Core)**

<img width="1894" height="990" alt="Screenshot 2025-07-20 183815" src="https://github.com/user-attachments/assets/7ffb06d3-4052-4b29-90dc-37422fffa032" />
<img width="1919" height="1009" alt="Screenshot 2025-07-20 183839" src="https://github.com/user-attachments/assets/220d9caf-a2fb-430c-9fde-6caed0798033" />
