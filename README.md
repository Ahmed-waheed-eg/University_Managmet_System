# 🚀 Unviversty Mangment System Web API: (in progress)

![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![C#](https://custom-icon-badges.demolab.com/badge/C%23-%23239120.svg?logo=cshrp&style=for-the-badge&logoColor=white)
![JSON](https://img.shields.io/badge/JSON-000?logo=json&style=for-the-badge&logoColor=white)

<p align="center">
  <b>Unviversty Mangment System built with ASP.NET Core API designed with a focus on Clean Architecture.</b>
</p>

## 📚 Project Overview
The University Management System is an API-based project developed to handle core operations within a university environment. The system is designed to manage students, doctors, admins, and super admins, each with role-specific functionalities and permissions.
The system provides APIs for student registration, academic management, course handling and exam-related data, while also supporting administrative control through the Admin and SuperAdmin roles.
The project follows Onion Architecture and applies Repository & Service patterns to ensure clean separation of concerns and scalability. Data is stored and managed in SQL Server using Entity Framework Core.

## 🌟 Features:

- Student registration, academic records, and enrollment.
- Doctor management and course assignments.
- Admin and SuperAdmin control over departments, courses, and system users.
- Exam scheduling and grade recording.
- Role-based authentication and authorization (Student, Doctor, Admin, SuperAdmin).

---
##🔹Tech Stack:
- Backend: ASP.NET Core Web API
- Architecture: Onion Architecture, Repository & Service Pattern
- Database: SQL Server with EF Core
- Security: Role-based authentication and authorization
---

## 💎 Prerequisites
- [.NET 8](https://dotnet.microsoft.com/pt-br/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Git](https://git-scm.com/)
---

## 🎇 Setup and Configuration

1. **Clone the Repository**:
```bash
git clone "https://github.com/Ahmed-waheed-eg/University_Managmet_System.git"
```

2. **Restore Dependencies**:
```bash
dotnet restore
```

3. **Update Database Connection**:
Edit `appsettings.json` with your SQL Server connection string.

4. **Apply Migrations**:
```bash
dotnet ef database update
```

5. **Run the Application**:

Access the API at `https://localhost:5001`.

---

---

## 🛠 Technologies Used
- **Backend**: ASP.NET Core 9, Entity Framework Core.
- **Database**: SQL Server.
- **Security**: JWT Tokens.
- **Design Patterns**: Repository, Unit of Work, Specification.

---

## 🤝 Contribution
Contributions are welcome! Fork the repository, make changes, and submit a pull request.


---

## 📬 Contact
- **Email**: [ahmed.waheed.abw@gmail.com](mailto:ahmed.waheed.abw@gmail.com)
- **GitHub**: [Ahmed-Waheed](https://github.com/Ahmed-waheed-eg)
- **LinkedIn**: [Ahmed-Waheed](https://www.linkedin.com/in/ahmed-waheed-abw)
