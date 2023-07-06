namespace Mservices.Identity;

public record TokenGenerationRequest(Guid UserId, string Email, IDictionary<string, object> CustomClaims);