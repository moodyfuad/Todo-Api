# 📝 ToDo Collaborative API  
A clean, modular, and scalable **ASP.NET Core Web API** built to manage collaborative task lists with roles, permissions, and activity tracking.

This project demonstrates strong Full Stack and backend architecture skills using **Clean Architecture**, **EF Core**, **JWT Authentication**, and **Role-Based Access Control**.

---

## 🚀 Features

### ✅ Core Modules
- **Task Lists Management**  
  - Create, update, delete, and view task lists  
  - Assign roles & permissions for each list  

- **Tasks Management**  
  - CRUD operations for tasks inside each list  
  - Track progress & completion state  

- **Notes Management**  
  - Add notes to specific tasks  
  - Edit & delete notes  
  - Linked to task-level permissions  

### 👥 Members & Permissions
Role-based access per list with **three structured roles**:
- **Admin** – Full control over list, tasks, notes, and members  
- **Editor** – Can modify tasks & notes  
- **Viewer** – Read-only access  

Each role enforces capabilities through middleware-level permission checks.

---

## 🔔 Activity Logging (Notifications)
Every action in the system is recorded as an Activity Log:
- Task Created / Updated / Deleted  
- Note Created / Updated / Deleted  
- Member Added / Role Updated  
- List Modified  

These logs are exposed via a dedicated API endpoint and can be consumed by any client (Blazor/Flutter).

---

## 🏛 Architecture
The project follows **Onion Architecture**:

