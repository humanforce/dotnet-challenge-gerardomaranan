# Humanforce .NET take home challenge

# Stack
### Deps
- CleanArchitecture
    - MediatR
    - AutoMapper
    - FluentValidation
    - FluentAssertions
    - NUnit
    - Respawn
- Postgresql

### Setup
 - Generating Migrations
 ```
 $ dotnet ef migrations add "RemovedAppointmentDoctorId" --project src/Infrastructure --startup-project src/Web --output-dir Data/Migrations
 ```

 - Running Migrations
  ```
 $ dotnet ef database update --project src/Web
 ```

  - Running Tests
  ```
 $ dotnet test 
 ```

  - Running Development (localhost)
  ```
 $ dotnet run --project src/Web
 ```


### Play
1. Run API
2. Go to `localhost:5001` for Swagger openapi doc
3. Generate Bearer token through `api/users/login` using credentials seeded from [ApplicationDbContextIntiialiser.cs](https://github.com/humanforce/dotnet-challenge-gerardomaranan/blob/f7becb1b98dab6c83ba0562b0a15c74d412675fc/src/Infrastructure/Data/ApplicationDbContextInitialiser.cs#L82)
4. Have fun testing




---

### Appointment Scheduling API Design

## Objective:
Design and implement a REST API using C# and .NET, focusing on clean architecture, scalability, and extensibility. This challenge will assess your technical decision making skills, engineering ability, and approach to higher-level system design.

## Scenario:
You’re tasked with building an Appointment Scheduling API for a medical clinic. The system will manage patient appointments, doctor availability, and appointment statuses.

## Requirements:
### Implement API endpoints for following core features:

> Note: Ensure that some sample data is added to demonstrate API functionality.

1. Implement the API including a database with the following endpoints and entities.

    #### Entities:
    - **Patient**: ID, Name, Date of Birth, Phone number, email address
    - **Doctor**: ID, Name, Specialty, Available Time Slots
    - **Appointment**: ID, Patient ID, Doctor ID, Scheduled Time, Status (Scheduled, Completed, Cancelled)

    #### API Endpoints:

    1. Create, update, and cancel an appointment
    2. Get available time slots for a specific doctor in a given specific date.
    3. Retrieve a patient’s appointment history
    4. Get a daily schedule of appointments for a doctor
    5. Generate a summary report of appointments by status (e.g., total scheduled, completed, cancelled) for a given time period

2. Include a postman collection with the submission to enable easy testing and evaluation of the API.

### Bonus Points (Optional):

- Implement simple authentication.
- Add pagination and sorting for appointment and patient queries.
- Include an additional endpoint for doctor availability across multiple days.


### Technical Expectations:

- **Language & Frameworks**: Use C# and .NET 6 or later. You are free to use any third party packages where appropriate.
- **Architecture**: Structure your solution using clean architecture principles.
- **Tests** Include appropriate unit tests.
- **Documentation**: Document any design decisions and assumptions.
- **Error Handling**: Implement meaningful error messages and proper HTTP status codes.


### System Design Considerations:
> **Note**: These are some conversation points that may be covered in a final interview. 

- **Scalability**: How would the system handle a high volume of appointment bookings and queries?

- **Extensibility**: How would you design this API to support future features like telehealth appointments, reminders, or recurring appointments?

- **Data Storage**: How would you model the data to avoid performance bottlenecks and maintain consistency?





## Submission Guidelines
1. Provide any additional setup instructions
2. Include comments explaining complex logic
3. Advise our talent acquisition team once you have completed the assessment so that our team can begin their evaluation.

### Time Expectation:
4-6 hours.

### Evaluation Criteria:

- Code quality and organisation.
- System design and scalability considerations.
- Clarity of documentation and communication.
- Testing approach.
- Thoughtfulness around extensibility and future needs.

## Questions?
If you have any questions or require any clarifications, please reach out to your hiring manager or our talent acquisition team.


## **🎉 Good luck and happy coding! 🎉**
