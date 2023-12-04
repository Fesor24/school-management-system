using SMS.Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Domain.DomainEvents;
public sealed record CourseCreatedEvent(string CourseName, string CourseCode, int Unit) : IDomainEvent;
