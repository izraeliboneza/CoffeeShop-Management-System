# CoffeeShop Management System

---

## Repository

The project repository is available here:  

https://github.com/izraeliboneza/CoffeeShop-Management-System

---

## Project Overview

This project is a **console-based CoffeeShop Management System.**

The system simulates a real-world **Point of Sale (POS)** workflow for a coffee shop.
Employees can log in, create and manage orders, process payments, view order history, generate sales reports, and track work sessions.

The main goal of the project is to demonstrate object-oriented programming, clean structure, unit testing, file handling, and Git/GitHub collaboration in a practical backend system.

---

## Project Structure

```text
CoffeeShopManagementSystem/
│
├── Data/
│   ├── orders.json
│   └── work_sessions.json
│
├── Entities/
│   ├── Barista.cs
│   ├── Coffee.cs
│   ├── Employee.cs
│   ├── Order.cs
│   ├── OrderItem.cs
│   ├── Supervisor.cs
│   └── WorkSession.cs
│
├── Interfaces/
│   ├── IFileService.cs
│   └── IPaymentProcessor.cs
│
├── Services/
│   ├── EmployeeService.cs
│   ├── JsonFileService.cs
│   ├── LoginService.cs
│   ├── MenuService.cs
│   ├── OrderService.cs
│   ├── PaymentProcessors.cs
│   ├── ReportService.cs
│   └── WorkSessionService.cs
│
├── Utils/
│   └── InputValidator.cs
│
├── Program.cs
├── README.md
│
├── CoffeeShopManagementSystem.Tests/
│   ├── FakeFileService.cs
│   ├── OrderServiceTests.cs
│   ├── OrderTests.cs
│   ├── PaymentProcessorTests.cs
│   └── ReportServiceTests.cs
```

This structure provides separation of concerns and improves maintainability.

---

## Sample Data

The JSON files included in the `Data/` folder contain pre-generated sample data for testing and demonstration purposes.
This allows the system to be tested immediately without requiring manual input.

The data simulates:
- Completed orders
- Multiple employees
- Various payment methods
- Work sessions and time tracking

The system will continue to append new data during runtime.

No additional setup is required to start testing the system.

---

## How to Run

1. Open the project in Rider or Visual Studio
2. Build the solution
3. Run the application
4. Log in using an employee ID

---

## How to Run Tests

The project includes a separate xUnit test project:

```text
CoffeeShopManagementSystem.Tests
```

### Rider

1. Right-click the test project
2. Select **Run Tests**

### Visual Studio

1. Open **Test Explorer**
2. Run all tests

### Terminal

If using the terminal:

```bash
dotnet test
```

---

## Test Strategy

The tests are designed to verify the most important parts of the system.

The project uses the AAA pattern:

```text
Arrange
Act
Assert
```

### Test Isolation

To avoid dependency on file storage during testing, a `FakeFileService` is used.

This is a mock implementation of `IFileService` that stores data in memory instead of writing to JSON files.
This ensures that tests are fast, reliable, and independent of external systems.

The use of `FakeFileService` also allows proper testing of the `OrderService` and `ReportService` without side effects.

### Example Test Areas

- Order creation
- Adding items to orders
- Adjusting item quantities
- Removing items from orders
- Total price calculation
- Payment processing
- Cash payment validation
- Report calculations
- Order history logic
- Work session related logic

The tests help ensure that changes in the code do not break existing functionality.

---

## Technical Summary

The project demonstrates:

- Object-oriented programming
- Role-based system behavior
- Interface-based payment handling
- JSON file persistence
- Unit testing
- Clean Code structure
- Git/GitHub collaboration
- Realistic console application flow

---

## Notes

- The system uses JSON instead of a database
- Orders and work sessions are stored in separate files
- Payment is simulated for educational purposes
- The application focuses on backend logic and system structure
- The project is designed to meet the assignment requirements while also including extra functionality

---

## Roles and Responsibilities
> *Note: Although roles and responsibilities were clearly defined, the development process was highly collaborative.
> Team members worked closely together throughout the project, contributing across different parts of the system beyond their primary roles.
> This included supporting each other with problem-solving, reviewing code, assisting with implementation,
> and ensuring overall system quality and progress...*

### Kjell-Ivar Pettersen
**Team Lead / Project Manager / Technical Lead / Developer**

Responsible for leading the project both organizationally and technically, ensuring structured progress, clear communication, and high code quality throughout development.

---

### Team Lead / Project Manager

Responsible for planning, coordination, and overall project management.

**Responsibilities:**

- Organize and lead team meetings
- Coordinate team workload and collaboration
- Manage timeline and overall project progress
- Set up and maintain sprint planning
- Follow up on deadlines and deliverables
- Ensure steady progress throughout the development process
- Communicate with the instructor and clarify requirements

---

### Developer / Technical Lead

Responsible for technical implementation, architecture, and code quality.

**Responsibilities:**

- Implement core system features
- Design and maintain system architecture
- Ensure clean, structured, and maintainable code
- Perform code reviews and ensure consistency across the project
- Maintain high code quality standards
- Contribute to development of main system functionality
- Support and assist team members with technical challenges

---


### Izraeli Boneza
**DevOps & Git Responsible**

Responsible for managing version control and ensuring a structured and efficient workflow throughout the project using Git and GitHub.

**Responsibilities:**

- Set up and maintain the project repository on GitHub
- Define and enforce a consistent branching strategy
- Manage feature branches, integration, and merging processes
- Assist in resolving merge conflicts and maintaining code stability
- Support team members with Git-related issues and workflows
- Ensure that all team members contributed through commits
- Maintain clean and meaningful commit history
- Monitor repository structure and organization


---

### Øyvind Westad Olsen
**Tester & Documentation Responsible**

Responsible for testing and documentation throughout the project, ensuring that the system met both functional and quality requirements.

**Responsibilities:**

- Design, write, and maintain unit tests using xUnit
- Ensure that all core functionality is tested and behaves as expected
- Follow the AAA pattern (Arrange, Act, Assert) in test implementation
- Validate system behavior through both automated and manual testing
- Identify and report bugs or inconsistencies during development
- Contribute to and maintain project documentation



> *Note: Unfortunately, due to illness, Øystein`s participation was limited during the final stages of the project,
> and the team took over his responsibilities to ensure the project was completed.*

---

## Final Reflection

This project shows how a console-based backend system can be structured using object-oriented programming and clean architecture.

Although the system is built for educational purposes, it includes several realistic features found in real POS systems, such as role-based access, order handling, payment simulation, sales reporting, JSON persistence, and employee work session tracking.

The project meets the mandatory assignment requirements and also includes several bonus elements such as LINQ usage, collections, clean code principles, documentation, and extended system functionality.

---