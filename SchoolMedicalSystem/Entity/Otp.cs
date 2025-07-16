using System;
using System.Collections.Generic;

namespace SchoolMedicalSystem.Entity;

public partial class Otp
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string OtpCode { get; set; } = null!;

    public int FailedAttempts { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsUsed { get; set; }
}
