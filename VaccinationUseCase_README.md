# Vaccination Event Management System

## Overview
This vaccination use case provides a comprehensive solution for managing vaccination events in the School Medical System. It allows managers to create vaccination events, send email notifications to parents, collect parent responses, and generate reports.

## Features

### For Managers (Admin/Manager Role)
- ✅ **CRUD Operations** for vaccination events
- ✅ **Email Notifications** to all parents or specific parents
- ✅ **Response Tracking** and statistics
- ✅ **Event Management** with date ranges and upcoming events
- ✅ **Comprehensive Reporting** with confirmation rates

### For Parents
- ✅ **Email Notifications** with personalized links
- ✅ **Easy Response System** to accept/decline vaccination
- ✅ **Reason Documentation** for declining
- ✅ **No Authentication Required** for responses

## Database Schema Changes

### Updated Entities

#### Vaccinationevent
```sql
-- Added new fields
ALTER TABLE Vaccinationevent ADD EventDate datetime;
ALTER TABLE Vaccinationevent ADD Description nvarchar(max);
```

#### Vaccinationrecord
```sql
-- Added new fields for parent response tracking
ALTER TABLE Vaccinationrecord ADD WillAttend bit NULL;
ALTER TABLE Vaccinationrecord ADD ReasonForDecline nvarchar(max) NULL;
ALTER TABLE Vaccinationrecord ADD ParentConsent bit NULL;
ALTER TABLE Vaccinationrecord ADD ResponseDate datetime NULL;
```

## API Endpoints

### Vaccination Event Management

#### 1. Get All Events
```http
GET /api/VaccinationEvent
Authorization: Bearer {token}
```

#### 2. Get Event by ID
```http
GET /api/VaccinationEvent/{id}
Authorization: Bearer {token}
```

#### 3. Create New Event
```http
POST /api/VaccinationEvent
Authorization: Bearer {token}
Content-Type: application/json

{
  "vaccinationEventName": "Tiêm chủng COVID-19 cho học sinh",
  "location": "Phòng y tế trường học",
  "organizedBy": "Trung tâm Y tế Quận 1",
  "eventDate": "2024-12-15T08:00:00",
  "description": "Tiêm chủng vaccine COVID-19 cho học sinh từ lớp 1-5"
}
```

#### 4. Update Event
```http
PUT /api/VaccinationEvent/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "vaccinationEventId": 1,
  "vaccinationEventName": "Tiêm chủng COVID-19 cho học sinh (Cập nhật)",
  "location": "Phòng y tế trường học",
  "organizedBy": "Trung tâm Y tế Quận 1",
  "eventDate": "2024-12-20T08:00:00",
  "description": "Tiêm chủng vaccine COVID-19 cho học sinh từ lớp 1-5 - Đã cập nhật"
}
```

#### 5. Delete Event
```http
DELETE /api/VaccinationEvent/{id}
Authorization: Bearer {token}
```

### Event Queries

#### 6. Get Upcoming Events
```http
GET /api/VaccinationEvent/upcoming
Authorization: Bearer {token}
```

#### 7. Get Events by Date Range
```http
GET /api/VaccinationEvent/date-range?startDate=2024-12-01&endDate=2024-12-31
Authorization: Bearer {token}
```

#### 8. Get Event Summary
```http
GET /api/VaccinationEvent/{id}/summary
Authorization: Bearer {token}
```

#### 9. Get Student Responses
```http
GET /api/VaccinationEvent/{id}/responses
Authorization: Bearer {token}
```

### Email Management

#### 10. Send Email to All Parents
```http
POST /api/VaccinationEvent/{id}/send-email
Authorization: Bearer {token}
Content-Type: application/json

{
  "vaccinationEventId": 1,
  "emailTemplateId": 1,
  "customMessage": "Vui lòng phản hồi sớm để chúng tôi có thể lập kế hoạch phù hợp."
}
```

#### 11. Send Email to Specific Parents
```http
POST /api/VaccinationEvent/{id}/send-email-specific?parentIds=1&parentIds=2&parentIds=3
Authorization: Bearer {token}
Content-Type: application/json

{
  "vaccinationEventId": 1,
  "emailTemplateId": 1,
  "customMessage": "Nhắc nhở: Vui lòng phản hồi về sự kiện tiêm chủng."
}
```

### Parent Response (No Authentication Required)

#### 12. Process Parent Response
```http
POST /api/VaccinationEvent/respond
Content-Type: application/json

{
  "parentId": 1,
  "studentId": 5,
  "vaccinationEventId": 1,
  "willAttend": true,
  "reasonForDecline": null,
  "parentConsent": true
}
```

#### 13. Get Response Form (for email links)
```http
GET /api/VaccinationEvent/respond?email=parent@example.com&eventId=1
```

### Statistics and Reporting

#### 14. Get Event Statistics
```http
GET /api/VaccinationEvent/{id}/statistics
Authorization: Bearer {token}
```

#### 15. Get Events with Statistics
```http
GET /api/VaccinationEvent/with-statistics
Authorization: Bearer {token}
```

#### 16. Get Parent Responses
```http
GET /api/VaccinationEvent/{id}/parent-responses
Authorization: Bearer {token}
```

## Email Templates

The system includes two built-in email templates:

### 1. Default Vaccination Email Template
- Professional HTML formatting
- Event details with placeholders
- Call-to-action button
- Important notes section

### 2. Reminder Email Template
- Urgent styling for reminders
- Simplified content
- Strong call-to-action

### Email Placeholders
- `{EventName}` - Name of the vaccination event
- `{EventDate}` - Date of the event
- `{Location}` - Event location
- `{Description}` - Event description
- `{CustomMessage}` - Custom message from manager
- `{ResponseLink}` - Generated response link
- `{ParentName}` - Parent's name (for specific emails)

## Usage Examples

### Complete Workflow Example

#### Step 1: Create Vaccination Event
```json
POST /api/VaccinationEvent
{
  "vaccinationEventName": "Tiêm chủng định kỳ tháng 12/2024",
  "location": "Phòng y tế trường học",
  "organizedBy": "Trung tâm Y tế Quận 1",
  "eventDate": "2024-12-15T08:00:00",
  "description": "Tiêm chủng vaccine cúm và các vaccine định kỳ khác cho học sinh"
}
```

#### Step 2: Send Email Notifications
```json
POST /api/VaccinationEvent/1/send-email
{
  "vaccinationEventId": 1,
  "emailTemplateId": 1,
  "customMessage": "Vui lòng phản hồi trong vòng 3 ngày để chúng tôi có thể lập kế hoạch chi tiết."
}
```

#### Step 3: Monitor Responses
```http
GET /api/VaccinationEvent/1/summary
```

#### Step 4: Send Reminders (if needed)
```json
POST /api/VaccinationEvent/1/send-email-specific?parentIds=1&parentIds=2
{
  "vaccinationEventId": 1,
  "emailTemplateId": 2,
  "customMessage": "Nhắc nhở: Chưa nhận được phản hồi của bạn."
}
```

## Response Data Models

### VaccinationEventDTO
```json
{
  "vaccinationEventId": 1,
  "vaccinationEventName": "Tiêm chủng COVID-19",
  "location": "Phòng y tế",
  "organizedBy": "Trung tâm Y tế",
  "eventDate": "2024-12-15T08:00:00",
  "description": "Tiêm chủng vaccine COVID-19",
  "createdDate": "2024-11-01T10:00:00",
  "modifiedDate": "2024-11-01T10:00:00",
  "createdBy": "admin",
  "modifiedBy": "admin",
  "isDeleted": false,
  "totalStudents": 150,
  "confirmedCount": 120,
  "declinedCount": 15,
  "pendingCount": 15
}
```

### VaccinationEventSummaryDTO
```json
{
  "vaccinationEventId": 1,
  "vaccinationEventName": "Tiêm chủng COVID-19",
  "eventDate": "2024-12-15T08:00:00",
  "location": "Phòng y tế",
  "totalStudents": 150,
  "confirmedCount": 120,
  "declinedCount": 15,
  "pendingCount": 15,
  "confirmationRate": 80.0,
  "studentResponses": [
    {
      "studentId": 1,
      "studentName": "Nguyễn Văn A",
      "parentName": "Nguyễn Văn B",
      "parentEmail": "parent@example.com",
      "className": "Lớp 1A",
      "willAttend": true,
      "reasonForDecline": null,
      "responseDate": "2024-11-02T14:30:00",
      "status": "Confirmed"
    }
  ]
}
```

## LINQ Query Examples

The system uses EF Core LINQ queries extensively:

### Complex Join Query for Student Responses
```csharp
var query = from s in _context.Students
           join p in _context.Parents on s.Parentid equals p.Parentid
           join c in _context.Classrooms on s.Classid equals c.Classid
           join vr in _context.Vaccinationrecords on s.Studentid equals vr.Studentid into vrGroup
           from vr in vrGroup.DefaultIfEmpty()
           where !s.IsDeleted && !p.IsDeleted && !c.IsDeleted
           && (vr == null || (vr.Vaccinationeventid == eventId && !vr.Isdeleted))
           select new StudentVaccinationStatusDTO
           {
               StudentId = s.Studentid,
               StudentName = s.Fullname,
               ParentName = p.Fullname,
               ParentEmail = p.Email ?? "",
               ClassName = c.Classname,
               WillAttend = vr.WillAttend,
               ReasonForDecline = vr.ReasonForDecline,
               ResponseDate = vr.ResponseDate,
               Status = vr == null ? "Pending" :
                       vr.WillAttend == true ? "Confirmed" :
                       vr.WillAttend == false ? "Declined" : "Pending"
           };
```

### Statistics Queries
```csharp
// Confirmed count
var confirmedCount = await _context.Vaccinationrecords
    .CountAsync(r => r.Vaccinationeventid == eventId && r.WillAttend == true && !r.Isdeleted);

// Pending count (total students - responded students)
var totalStudents = await _context.Students.CountAsync(s => !s.IsDeleted);
var respondedStudents = await _context.Vaccinationrecords
    .Where(r => r.Vaccinationeventid == eventId && !r.Isdeleted)
    .Select(r => r.Studentid)
    .Distinct()
    .CountAsync();
var pendingCount = totalStudents - respondedStudents;
```

## Security Features

- **Role-based Authorization**: Only Admin/Manager can create/update/delete events
- **JWT Authentication**: Required for all management operations
- **Anonymous Parent Responses**: Parents can respond without authentication
- **Input Validation**: Comprehensive validation on all DTOs
- **SQL Injection Protection**: EF Core parameterized queries

## Error Handling

The system includes comprehensive error handling:

- **Validation Errors**: Returns 400 Bad Request with detailed error messages
- **Not Found Errors**: Returns 404 for missing resources
- **Authorization Errors**: Returns 401/403 for unauthorized access
- **Server Errors**: Returns 500 with generic error messages (logs actual errors)

## Performance Considerations

- **Eager Loading**: Uses Include() for related data
- **Async Operations**: All database operations are async
- **Efficient Queries**: Optimized LINQ queries with proper indexing
- **Pagination Ready**: Structure supports future pagination implementation

## Future Enhancements

1. **SMS Notifications**: Add SMS support for urgent reminders
2. **Bulk Operations**: Support for bulk email sending and response processing
3. **Advanced Reporting**: Export to Excel/PDF functionality
4. **Mobile App Integration**: API ready for mobile app development
5. **Real-time Updates**: SignalR integration for live updates
6. **Multi-language Support**: Internationalization for email templates

## Testing

### Sample Test Data
```sql
-- Insert test vaccination event
INSERT INTO Vaccinationevent (Vaccinationeventname, Location, Organizedby, EventDate, Description, Createddate, Modifieddate, Createdby, Modifiedby, Isdeleted)
VALUES ('Test Vaccination Event', 'Test Location', 'Test Organizer', '2024-12-15 08:00:00', 'Test Description', GETDATE(), GETDATE(), 'test', 'test', 0);

-- Insert test vaccination record
INSERT INTO Vaccinationrecord (Studentid, Vaccinationeventid, Vaccinename, Dosenumber, Vaccinationdate, Confirmedbyparent, WillAttend, ParentConsent, ResponseDate, Isdeleted, Createdat, Updatedat, Createdby, Updatedby)
VALUES (1, 1, 'Test Vaccine', 1, '2024-12-15', 1, 1, 1, GETDATE(), 0, GETDATE(), GETDATE(), 'test', 'test');
```

This vaccination use case provides a complete, production-ready solution for managing vaccination events in the School Medical System with comprehensive email functionality, parent response tracking, and detailed reporting capabilities. 