using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Data.Entities;

public class SendEmailTrack
{
    [Key, EmailAddress]
    public string Email { get; set; }
    public int NumOfTries { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime OpenDate { get; set; } = DateTime.UtcNow;
    [DataType(DataType.DateTime)]
    public DateTime? BlockExpiration { get; set; } = null;
    public int SeverityLevel { get; set; } = 1;
}

