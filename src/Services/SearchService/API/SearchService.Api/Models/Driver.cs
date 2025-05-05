using Newtonsoft.Json;

namespace SearchService.Api.Models;

public record Driver(
    string id,
    string FirstName,
    string LastName,
    string demo,
    DateTime CreatedAt
    );